using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

using System.Data.SqlClient;
using System.Data;
using System.IO;

using System.Security.Cryptography;

namespace DBAccess
{
    /*
    public class Form1 : System.Windows.Forms.Form
    {
        private System.ComponentModel.Container components = null;
        private Image theImage;

        public Form1(Image image)
        {
            InitializeComponent();
            SetStyle(ControlStyles.Opaque, true);
            theImage = image;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(theImage, ClientRectangle);
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Size = new System.Drawing.Size(300, 300);
            this.Text = "Form1";
        }
    }
     */

    public class AccesDonnees
    {
        System.Data.SqlClient.SqlConnection connexion;

        /*
        [STAThread]
        static void Main(string[] args)
        {
            //Class1.ecrireCoucou();
            //while (true) ;

            AccesDonnees p = new AccesDonnees();
        }
        */

        public AccesDonnees()
        {
            // Connect to DB
            String connectionString = "Server = NOVAE\\SQLEXPRESS; Database = MiniProjetDB; Trusted_Connection = True;";
            connexion = new System.Data.SqlClient.SqlConnection(connectionString);

            // Get image path

            // Store image in DB
            /*
            OpenFileDialog f = new OpenFileDialog();
            f.ShowDialog();
            storeImage(f.FileName, "toto");
             */

            // Get image from DB
            /*
            byte[] data = getImage("toto");
            MemoryStream stream = new MemoryStream(data);
            Image image = Image.FromStream(stream);

            Application.Run(new Form1(image));
             */
        }

        // Gets the sha1 of the image using its bytes array
        private String sha1(byte[] bytes)
        {
            SHA1CryptoServiceProvider serv = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(serv.ComputeHash(bytes)).Replace("-", "");
        }

        // Stocke l'image situee a "path" dans la BDD avec le nom "name"
        public void storeImage(String path, String name)
        {
            //Image image = Image.FromFile(@"C:\image.jpg");
            Image image = Image.FromFile(path);
            byte[] imageData = Image2ByteArray(image);

            addImage(imageData, name);
        }

        private Byte[] Image2ByteArray(Image img)
        {
            MemoryStream mstImage = new MemoryStream();
            img.Save(mstImage, System.Drawing.Imaging.ImageFormat.Jpeg);
            Byte[] bytImage = mstImage.GetBuffer();
            return bytImage;
        }

        public int createAlbum(String name, String user)
        {
            // On vérifie que l'utilisateur existe bien
            if (!userExists(user))
            {
                return -2;
            }

            int idUser = getUserId(user);

            try
            {
                connexion.Open();

                SqlCommand ajoutAlbum = new SqlCommand("INSERT INTO album(name) VALUES(@name);", connexion);

                ajoutAlbum.Parameters.Add("@name", SqlDbType.VarChar, name.Length).Value = name;

                if (ajoutAlbum.ExecuteNonQuery() <= 0)
                    return -1;

                SqlCommand getLastId = new SqlCommand("SELECT IDENT_CURRENT('album')", connexion);
                int idAlbum = Convert.ToInt32(getLastId.ExecuteScalar());

                // On donne le droit à l'utilisateur de voir son album
                if (idUser < 0)
                    return -2;

                if (!grantPrivilege(idUser, idAlbum))
                {
                    return -3;
                }

                return idAlbum;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return -1;
        }

        public bool deleteAlbum(int id)
        {
            try
            {
                connexion.Open();

                SqlCommand suppressionAlbum = new SqlCommand("DELETE FROM Album WHERE id=@id", connexion);
                suppressionAlbum.Parameters.Add("@id", SqlDbType.Int).Value = id;
                if (suppressionAlbum.ExecuteNonQuery() > 0)
                {
                    // On supprime les références à l'album de user_album
                    SqlCommand suppressionUserAlbum = new SqlCommand("DELETE FROM user_album WHERE id_album=@id", connexion);
                    suppressionUserAlbum.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    if (suppressionUserAlbum.ExecuteNonQuery() <= 0)
                        return false;

                    // On supprime les références à l'album dans album_photo
                    SqlCommand suppressionAlbumPhoto = new SqlCommand("DELETE FROM album_photo WHERE id_album=@id", connexion);
                    suppressionAlbumPhoto.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    suppressionAlbumPhoto.ExecuteNonQuery();

                    // On supprime toutes les photos dont l'ID n'est pas dans album_photo.id_photo
                    SqlCommand cleanUp = new SqlCommand("DELETE FROM image WHERE image.hash NOT IN album_photo.id_photo", connexion);
                    cleanUp.ExecuteNonQuery();

                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return false;
        }

        public int addUser(String login, String password)
        {
            try
            {
                connexion.Open();

                SqlCommand ajoutUtilisateur = new SqlCommand("INSERT INTO [User](login, password) VALUES(@login, @password);", connexion);

                ajoutUtilisateur.Parameters.Add("@login", SqlDbType.VarChar, login.Length).Value = login;
                ajoutUtilisateur.Parameters.Add("@password", SqlDbType.VarChar, password.Length).Value = password;

                if (ajoutUtilisateur.ExecuteNonQuery() <= 0)
                    return -1;

                SqlCommand getLastId = new SqlCommand("SELECT IDENT_CURRENT('user')", connexion);
                return Convert.ToInt32(getLastId.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return -1;
        }

        public bool deleteUser(String login)
        {
            int userId = getUserId(login);
            if (!deleteUserAlbums(userId))
                return false;

            try
            {
                connexion.Open();

                SqlCommand suppressionUtilisateur = new SqlCommand("DELETE FROM [User] WHERE login=@login", connexion);
                suppressionUtilisateur.Parameters.Add("@login", SqlDbType.VarChar, login.Length).Value = login;
                if (suppressionUtilisateur.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return false;
        }

        public bool deleteUserAlbums(int userId)
        {
            try
            {
                connexion.Open();
                SqlCommand getAlbums = new SqlCommand(
                "SELECT id_album " +
                "FROM user_album " +
                "WHERE id_user = @id_user", connexion);
                getAlbums.Parameters.Add("@id_user", SqlDbType.Int).Value = userId;

                SqlDataReader myReader = getAlbums.ExecuteReader(CommandBehavior.SequentialAccess);
                while(myReader.Read())
                {
                    // lit la taille du blob
                    int idAlbum = myReader.GetInt32(0);
                    deleteAlbum(idAlbum);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                // dans tous les cas on ferme la connexion
                connexion.Close();
            }
            return false;
        }

        public String uploadImage(byte[] image, String name, int album)
        {
            if (!albumExists(album))
                return null;

            String hash = addImage(image, name);
            if (hash == null)
                return null;

            if (!addPhotoToAlbum(hash, album))
                return null;

            return hash;
        }

        public String addImage(byte[] image, String name)
        {
            String hash = sha1(image);
            if (imageExists(hash))
                return hash;

            try
            {
                // connexion au serveur
                connexion.Open();

                // construit la requête
                SqlCommand ajoutImage = new SqlCommand(
                "INSERT INTO Image (hash, size, blob, name) " +
                "VALUES(@hash, @size, @blob, @name)", connexion);

                ajoutImage.Parameters.Add("@hash", SqlDbType.VarChar, hash.Length).Value = hash;
                ajoutImage.Parameters.Add("@size", SqlDbType.Int).Value = image.Length;
                ajoutImage.Parameters.Add("@blob", SqlDbType.Image, image.Length).Value = image;
                ajoutImage.Parameters.Add("@name", SqlDbType.VarChar, name.Length).Value = name;
                
                // execution de la requête
                if (ajoutImage.ExecuteNonQuery() > 0)
                    return hash;
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                // dans tous les cas on ferme la connexion
                connexion.Close();
            }

            return null;
        }

        public bool deleteImage(String hash)
        {
            try
            {
                connexion.Open();

                SqlCommand suppressionImage = new SqlCommand("DELETE FROM Image WHERE hash = @hash", connexion);
                suppressionImage.Parameters.Add("@hash", SqlDbType.VarChar, hash.Length).Value = hash;
                if (suppressionImage.ExecuteNonQuery() > 0)
                {
                    // On supprime les références à la photo de la table album_photo
                    SqlCommand supprRefImage = new SqlCommand("DELETE FROM album_photo WHERE id_photo = @hash", connexion);
                    supprRefImage.Parameters.Add("@hash", SqlDbType.VarChar, hash.Length).Value = hash;
                    if (supprRefImage.ExecuteNonQuery() <= 0)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return false;
        }

        private int getUserId(String userName)
        {
            try
            {
                connexion.Open();
                SqlCommand test = new SqlCommand("SELECT id FROM [User] WHERE login = @login", connexion);
                test.Parameters.Add("@login", SqlDbType.VarChar, userName.Length).Value = userName;
                SqlDataReader reader = test.ExecuteReader(CommandBehavior.SequentialAccess);
                if (!reader.HasRows)
                    return -1;
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
                    /*
                else
                {
                    return -1;
                }
                     */
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return -1;
        }

        private bool grantPrivilege(int idUser, int idAlbum)
        {
            // Vérifier que le couple n'existe pas déjà
            if (existsCouple("user_album", "id_user", idUser, "id_album", idAlbum))
            {
                return true;
            }

            // L'ajouter
            try
            {
                connexion.Open();

                SqlCommand ajoutCouple = new SqlCommand("INSERT INTO user_album(id_user, id_album) VALUES(@id_user, @id_album);", connexion);

                ajoutCouple.Parameters.Add("@id_user", SqlDbType.Int).Value = idUser;
                ajoutCouple.Parameters.Add("@id_album", SqlDbType.Int).Value = idAlbum;

                if (ajoutCouple.ExecuteNonQuery() <= 0)
                    return false;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return false;
        }

        private bool addPhotoToAlbum(String hash, int album)
        {
            // Vérifier que le couple n'existe pas déjà
            if (existsCouple("album_photo", "id_album", album, "id_photo", hash))
            {
                return true;
            }

            // L'ajouter
            try
            {
                connexion.Open();

                SqlCommand ajoutCouple = new SqlCommand("INSERT INTO album_photo(id_album, id_photo) VALUES(@id_album, @id_photo);", connexion);

                ajoutCouple.Parameters.Add("@id_album", SqlDbType.Int).Value = album;
                ajoutCouple.Parameters.Add("@id_photo", SqlDbType.VarChar, hash.Length).Value = hash;

                if (ajoutCouple.ExecuteNonQuery() <= 0)
                    return false;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return false;
        }

        private bool existsCouple(String table, String field1, int value1, String field2, int value2)
        {
            try
            {
                connexion.Open();
                SqlCommand test = new SqlCommand("SELECT * FROM " + table + " WHERE " + field1 + " = @" + field1 + 
                    " AND " + field2 + " = @" + field2 , connexion);
                test.Parameters.Add("@" + field1, SqlDbType.Int).Value = value1;
                test.Parameters.Add("@" + field2, SqlDbType.Int).Value = value2;
                SqlDataReader reader = test.ExecuteReader(CommandBehavior.SequentialAccess);
                return reader.HasRows;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return false;
        }

        private bool existsCouple(String table, String field1, int value1, String field2, String value2)
        {
            try
            {
                connexion.Open();
                SqlCommand test = new SqlCommand("SELECT * FROM " + table + " WHERE " + field1 + " = @" + field1 +
                    " AND " + field2 + " = @" + field2, connexion);
                test.Parameters.Add("@" + field1, SqlDbType.Int).Value = value1;
                test.Parameters.Add("@" + field2, SqlDbType.VarChar, value2.Length).Value = value2;
                SqlDataReader reader = test.ExecuteReader(CommandBehavior.SequentialAccess);
                return reader.HasRows;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return false;
        }

        private bool exists(String table, String field, String value)
        {
            try
            {
                connexion.Open();
                SqlCommand test = new SqlCommand("SELECT * FROM " + table + " WHERE " + field + " = @" + field, connexion);
                test.Parameters.Add("@"+field, SqlDbType.VarChar, value.Length).Value = value;
                SqlDataReader reader = test.ExecuteReader(CommandBehavior.SequentialAccess);
                return reader.HasRows;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return false;
        }

        private bool exists(String table, String field, int value)
        {
            try
            {
                connexion.Open();
                SqlCommand test = new SqlCommand("SELECT * FROM " + table + " WHERE " + field + " = @" + field, connexion);
                test.Parameters.Add("@" + field, SqlDbType.Int).Value = value;
                SqlDataReader reader = test.ExecuteReader(CommandBehavior.SequentialAccess);
                return reader.HasRows;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return false;
        }

        private bool imageExists(String hash)
        {
            return exists("Image", "hash", hash);
        }

        private bool userExists(String name)
        {
            return exists("[User]", "login", name);
        }

        private bool albumExists(int id)
        {
            return exists("Album", "id", id);
        }
        /*
        private bool imageExists(String hash)
        {
            try
            {
                connexion.Open();
                SqlCommand test = new SqlCommand("SELECT * FROM Image WHERE hash = @hash", connexion);
                test.Parameters.Add("@hash", SqlDbType.VarChar, hash.Length).Value = hash;
                SqlDataReader reader = test.ExecuteReader(CommandBehavior.SequentialAccess);
                return reader.HasRows;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return false;
        }
         */


        // récupération d'une image de la base à l'aide d'un DataReader
        public byte[] getImage(String hash)
        {
            byte[] blob = null;
            try
            {
                // connexion au serveur
                connexion.Open();
                // construit la requête
                SqlCommand getImage = new SqlCommand(
                "SELECT hash,size,blob,name " +
                "FROM Image " +
                "WHERE hash = @hash", connexion);
                getImage.Parameters.Add("@hash", SqlDbType.VarChar, hash.Length).Value = hash;
                // exécution de la requête et création du reader
                SqlDataReader myReader =
                getImage.ExecuteReader(CommandBehavior.SequentialAccess);
                if (myReader.Read())
                {
                    // lit la taille du blob
                    int size = myReader.GetInt32(1);
                    blob = new byte[size];
                    // récupére le blob de la BDD et le copie dans la variable blob
                    myReader.GetBytes(2, 0, blob, 0, size);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                // dans tous les cas on ferme la connexion
                connexion.Close();
            }
            return blob;
        }


    }
}
