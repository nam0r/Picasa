using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

using ClassLibrary1;
using System.Data.SqlClient;
using System.Data;
using System.IO;

using System.Windows.Forms;
using System.Security.Cryptography;

namespace ConsoleApplication1
{

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

    class Program
    {
        System.Data.SqlClient.SqlConnection connexion;

        [STAThread]
        static void Main(string[] args)
        {
            /*
            Class1.ecrireCoucou();
            while (true) ;
             */

            Program p = new Program();
        }

        public Program()
        {
            // Connect to DB
            String connectionString = "Server = OX\\SQLEXPRESS; Database = MiniProjetDB; Trusted_Connection = True;";
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
            try
            {
                MemoryStream mstImage = new MemoryStream();
                img.Save(mstImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                Byte[] bytImage = mstImage.GetBuffer();
                return bytImage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void addImage(byte[] image, String name)
        {
            String hash = sha1(image);
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
                ajoutImage.ExecuteNonQuery();
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
        }


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
