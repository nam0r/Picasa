using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DLLExxemple
{
    public class Class1
    {
        public static void ecrireCoucou()
        {
            Console.WriteLine("coucou");
        }

        public void addImage(String imageID, byte[] image)
        {
            SqlConnection connexion = new SqlConnection();
            connexion.ConnectionString = "Server=localhost;Initial Catalog=Picasa;uid=myLogin;password=myPassword";

            try
            {
                // connexion au serveur 
                connexion.Open();

                // construit la requête 
                SqlCommand ajoutImage = new SqlCommand(
                       "INSERT INTO Image (id, blob, size) " +
                       "VALUES(@id, @Blob, @size)", connexion);
                ajoutImage.Parameters.Add("@id", SqlDbType.VarChar, imageID.Length).Value = imageID;
                ajoutImage.Parameters.Add("@Blob", SqlDbType.Image, image.Length).Value = image;
                ajoutImage.Parameters.Add("@size", SqlDbType.Int).Value = image.Length;

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
    }
}
