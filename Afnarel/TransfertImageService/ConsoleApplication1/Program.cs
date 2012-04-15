using System;
using System.IO;
namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instanciation de la référence de service
            ImageTransfertServiceReference.ImageTransfertClient imageTransfertService = 
                new ImageTransfertServiceReference.ImageTransfertClient();
            MemoryStream imageStream = new MemoryStream(lireFichier(@"c:\image.jpg"));
            // Appel de notre web method
            Console.WriteLine("On essaie d'uploader une image.");
            imageTransfertService.UploadImage(imageStream);
            Console.Out.WriteLine("Transfert Terminé");
            Console.ReadLine();
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