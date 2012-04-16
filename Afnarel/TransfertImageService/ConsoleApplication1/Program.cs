using System;
using System.IO;
namespace ClientTest
{
    class Program
    {
        private const int CHOIX_MAX = 8;
        private ImageTransfertServiceReference.ImageTransfertClient service;

        public Program()
        {
            service = new ImageTransfertServiceReference.ImageTransfertClient();

            int choice;
            while ((choice = menu()) != CHOIX_MAX) // While not exit
            {
                switch (choice)
                {
                    case 1:
                        addUser();
                        break;
                    case 2:
                        deleteUser();
                        break;
                    case 3:
                        addPhoto();
                        break;
                    case 4:
                        deletePhoto();
                        break;
                    case 5:
                        createAlbum();
                        break;
                    case 6:
                        deleteAlbum();
                        break;
                    /*
                default:
                    Console.WriteLine("Ce choix n'existe pas");
                     */
                }
            }
        }

        static void Main(string[] args)
        {

            new Program();
            
            /*
            // Instanciation de la référence de service
            ImageTransfertServiceReference.ImageTransfertClient imageTransfertService = 
                new ImageTransfertServiceReference.ImageTransfertClient();
            MemoryStream imageStream = new MemoryStream(lireFichier(@"c:\image.jpg"));
            // Appel de notre web method
            imageTransfertService.UploadImage(imageStream);
            Console.Out.WriteLine("Transfert Terminé");
            Console.ReadLine();
             */
        }

        private void createAlbum()
        {
            Console.Write("Nom de l'album : ");
            String  name = Console.ReadLine();

            Console.Write("Nom de l'utilisateur proprietaire de l'album : ");
            String user = Console.ReadLine();

            int id;
            if ((id = service.CreateAlbum(name, user)) >= 0)
            {
                Console.WriteLine("L'album " + name + " a été correctement créé avec l'id " + id + ".");
            }
            else
            {
                Console.WriteLine("ID : " + id);
                Console.WriteLine("Une erreur est survenue lors de la création de l'album " + name + ".");
                switch (id)
                {
                    case -1:
                        Console.WriteLine("Pendant la création de l'album.");
                        break;
                    case -2:
                        Console.WriteLine("L'utilisateur " + user + " n'existe pas.");
                        break;
                    case -3:
                        Console.WriteLine("Erreur pendant l'affectation des privileges.");
                        break;
                }
            }
        }

        private void deleteAlbum()
        {
            int id;
            try
            {
                Console.Write("ID de l'album : ");
                id = Convert.ToInt32(Console.ReadLine());
            
                if (service.DeleteAlbum(id))
                {
                    Console.WriteLine("L'album " + id + " a été correctement supprimé.");
                }
                else
                {
                    Console.WriteLine("Une erreur est survenue lors de la suppression de l'album " + id + ".");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void deleteUser()
        {
            Console.WriteLine("Login : ");
            String login = Console.ReadLine();
            if (service.DeleteUser(login))
            {
                Console.WriteLine("L'utilisateur " + login + " a été correctement supprimé.");
            }
            else
            {
                Console.WriteLine("Une erreur est survenue lors de la suppression de l'utilisateur " + login + ".");
            }
        }

        private void addUser()
        {
            Console.Write("Login : ");
            String login = Console.ReadLine();
            Console.Write("Password : ");
            String password = Console.ReadLine();

            int id = service.AddUser(login, password);
            if (id == -1)
            {
                Console.WriteLine("Une erreur s'est produite lors de la création de l'utilisateur.");
            }
            else
            {
                Console.WriteLine("Le nouvel utilisateur a été créé avec l'id " + id);
            }
        }

        private void deletePhoto()
        {
            Console.Write("ID (hash) de l'image à supprimer : ");
            String hash = Console.ReadLine();
            if (service.DeleteImage(hash))
            {
                Console.WriteLine("L'image " + hash + " a été supprimée avec succès.");
            }
            else
            {
                Console.WriteLine("Une erreur s'est produite lors de la suppression de l'image " + hash);
            }
        }

        private void addPhoto()
        {
            Console.Write("Chemin vers l'image à ajouter : ");
            String filename = Console.ReadLine();
            Console.Write("Nom de l'image à ajouter : ");
            String name = Console.ReadLine();
            Console.Write("Album auquel ajouter l'image : ");
            int album = Convert.ToInt32(Console.ReadLine());

            try
            {
                MemoryStream imageStream = new MemoryStream(lireFichier(@"" + filename)); // c:\image.jpg
                ImageTransfertServiceReference.ImageInfo infos = new ImageTransfertServiceReference.ImageInfo();
                infos.Name = name;
                infos.Album = album;
                infos.ID = "";
                service.UploadImage(ref infos, imageStream);
                if (infos.ID == null)
                {
                    Console.Out.WriteLine("Le transfert a échoué.");
                }
                else
                {
                    Console.Out.WriteLine("Transfert Terminé : " + infos.ID + ".");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("L'image " + filename + " n'existe pas.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private int menu()
        {
            Console.WriteLine("================");
            Console.WriteLine("===== MENU =====");
            Console.WriteLine("================");
            Console.WriteLine("\t1. Créer un compte");
            Console.WriteLine("\t2. Supprimer un compte");
            Console.WriteLine("\t3. Ajouter une photo");
            Console.WriteLine("\t4. Supprimer une photo");
            Console.WriteLine("\t5. Créer un album.");
            Console.WriteLine("\t6. Supprimer un album");
            Console.WriteLine("\t7. Télécharger une photo");
            Console.WriteLine("\t8. Quitter");

            int choice = -1;
            bool ok = false;
            do
            {
                Console.Write("Votre choix : ");
                String input = Console.ReadLine();
                try
                {
                    choice = Convert.ToInt32(input);
                    if (choice >= 0 && choice <= CHOIX_MAX)
                    {
                        ok = true;
                    }
                    else
                    {
                        Console.WriteLine("Le choix doit être compris entre 1 et " + CHOIX_MAX + "...");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input string is not a sequence of digits.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("The number cannot fit in an Int32.");
                }
            } while (!ok);

            return choice;
            
        }
        /// <summary>
        /// Lit et retourne le contenu du fichier sous la forme de tableau de byte
        /// </summary>
        /// <param name="chemin">chemin du fichier</param>
        /// <returns></returns>
        public static byte[] lireFichier(string chemin)
        {
            byte[] data = null;
            FileInfo fileInfo = new FileInfo(chemin);
            int nbBytes = (int)fileInfo.Length;
            FileStream fileStream = new FileStream(chemin, FileMode.Open,
            FileAccess.Read);
            BinaryReader br = new BinaryReader(fileStream);
            data = br.ReadBytes(nbBytes);
            return data;
        }
    }
}