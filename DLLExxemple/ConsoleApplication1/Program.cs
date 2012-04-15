using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLLExxemple;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Class1.ecrireCoucou();
            OpenFileDialog f = new OpenFileDialog(); //création d'une fenetre d'exploration
            f.ShowDialog(); // affichage de cette fenetre
            String chemin = f.FileName; // on récupère l'adresse de l'image qu'on affecte à la varaiable chemin (string)

            PictureBox image = new PictureBox();
            Console.WriteLine(chemin);
            //image.ImageLocation = chemin ; 
            //image = 

            //Class1.addImage(1, image);
            while (true);
        }
    }
}
