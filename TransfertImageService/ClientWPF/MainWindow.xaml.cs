using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImageCollection imageCollection1;

        public MainWindow()
        {
            InitializeComponent();

            // On crée notre collection d'image et on y ajoute deux images 
            imageCollection1 = new ImageCollection();
            imageCollection1.Add(new ImageObjet("celestial",
            ClientTest.Program.lireFichier(@"D:\celestial.jpg")));
            imageCollection1.Add(new ImageObjet("pearlscale",
            ClientTest.Program.lireFichier(@"D:\pearlscale.jpg")));

            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML 
            ObjectDataProvider imageSource =
            (ObjectDataProvider)FindResource("ImageCollection1");
            imageSource.ObjectInstance = imageCollection1;
        }
    }

    public class ImageObjet
    {
        public String Nom { get; set; }
        public byte[] Image { get; set; }

        public ImageObjet(String Nom, byte[] Image)
        {
            this.Nom = Nom;
            this.Image = Image;
        }
    }

    public class ImageCollection : ObservableCollection<ImageObjet>
    { } 
}
