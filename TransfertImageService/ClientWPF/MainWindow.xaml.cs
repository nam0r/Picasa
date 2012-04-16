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
using System.Globalization;
using System.IO;
using System.Collections;
using System.ComponentModel;

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileCollection imageCollection1, imageCollection2;
        //l'objet de remontée de répertoire
        private FileObjet retourRepertoire, retourAlbum;
        // le répertoire actuel
        private String cheminActuel;
        // l'album actuel
        private int currentAlbum;


        public MainWindow(string initRep)
        {
            InitializeComponent();
            cheminActuel = initRep;
            retourRepertoire = new FileObjet("..");
            retourAlbum = new FileObjet("Albums");
            //on n'est dans aucun album au début
            currentAlbum = -1;
            
            // On crée notre collection d'image et on y ajoute deux images 
            imageCollection1 = new FileCollection();
            //foreach(Album al in getAlbums())
            //    imageCollection1.Add(new AlbumObjet(al.Title, al.Id));
            imageCollection1.Add(retourAlbum);
            imageCollection1.Add(new ImageObjet("celestial",
            ClientTest.Program.lireFichier(@"D:\celestial.jpg"), "ID1"));
            imageCollection1.Add(new ImageObjet("pearlscale",
            ClientTest.Program.lireFichier(@"D:\pearlscale.jpg"), "ID1"));
            imageCollection1.Add(new AlbumObjet("celestial", 3));

            imageCollection2 = new FileCollection();
            // On parcourt le répertoire courant à la recherche des images
            
            afficherImagesRépertoires(cheminActuel, imageCollection2);

            // On lie la collection au ObjectDataProvider déclaré dans le fichier XAML 
            ObjectDataProvider imageSource =
            (ObjectDataProvider)FindResource("ImageCollection1");
            imageSource.ObjectInstance = imageCollection1;

            // On lie la collection au ObjectDataProvider déclaré dans le fichier XAML 
            ObjectDataProvider imageSource2 =
            (ObjectDataProvider)FindResource("ImageCollection2");
            imageSource2.ObjectInstance = imageCollection2;
        }

        // permet de sonder un répertoire et de récolter les images et répertoires qui s'y trouvent dans une FileCollection
        private void afficherImagesRépertoires(String repertoire, FileCollection imageCollection)
        {
            imageCollection.Clear();
            imageCollection.Add(retourRepertoire);
            String[] fichiers = Directory.GetFiles(repertoire);
            String[] dossiers = Directory.GetDirectories(repertoire);
            for (int i = 0; i < dossiers.Length; i++)
            {
                imageCollection2.Add(new AlbumObjet(dossiers[i], 0));
            }
            for (int i = 0; i < fichiers.Length; i++)
            {
                if (fichiers[i].EndsWith(".jpg") || fichiers[i].EndsWith(".png") || fichiers[i].EndsWith(".gif"))
                    imageCollection.Add(new ImageObjet(fichiers[i], ClientTest.Program.lireFichier(@"" + fichiers[i]), "lol"));
            }
        }


        ListBox dragSource = null;

        // Fonction appelée si un objet est clické (elle sert pour le drag d'un drag&drop)
        private void ImageClickEvent(object sender, MouseButtonEventArgs e)
        {
            //Console.WriteLine(e.OriginalSource);

            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));
            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        // agit lorsqu'on sélectionne (et drag) un objet
        private void ImageClick(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            // si on a sélectionné une image, on ne fait rien ici
            AlbumObjet album = (AlbumObjet)e.Data.GetData(typeof(AlbumObjet));
            if (album != null)
            {
                Console.WriteLine("On a sélectionné un album.");
                //Si il s'agit d'un album (ne contient pas la commande de remontée de répertoire)
                if (!((IList)parent.ItemsSource).Contains(retourRepertoire))
                {
                    // On affiche les images de cet albumA
                    currentAlbum = album.Id;
                    //imageCollection1.Clear();
                    //imageCollection1.Add(retourAlbum);
                    //foreach(Image im in getImagesFromAlbum(album.Id))
                    //    imageCollection1.Add(new ImageObjet(im.Title, im.Image, im.Id));
                }
                //Si il s'agit d'un répertoire local
                else
                {
                   cheminActuel = album.Nom;
                   afficherImagesRépertoires(album.Nom, imageCollection2);
                }
            }

            //Si il s'agit de la commande de remontée d'un répertoire ou remontée vers un album
            FileObjet fichier = (FileObjet)e.Data.GetData(typeof(FileObjet));
            if (fichier != null)
            {
                // commande remontée d'un répertoire
                if (fichier.Equals(retourRepertoire))
                {
                    Console.WriteLine("cheminActuel : "+cheminActuel);

                    string[] split = cheminActuel.Split(new Char[] { '\\' });
                    if (split.Length > 1)
                    {
                        cheminActuel = "";
                        for (int i = 0; i < split.Length - 2; i++)
                        {
                            cheminActuel += split[i] + '\\';
                        }
                        cheminActuel += split[split.Length - 2];
                    }

                    Console.WriteLine("cheminActuel : " + cheminActuel);
                    afficherImagesRépertoires(cheminActuel, imageCollection2);
                }
                // commande retour aux albums
                else if (fichier.Equals(retourAlbum))
                {
                    // On affiche les albums
                    currentAlbum = -1;
                    //imageCollection1.Clear();
                    //foreach(Album al in getAlbums())
                    //    imageCollection1.Add(new AlbumObjet(al.Title, al.Id));
                }
            }
            
            // si on a sélectionné un album, on va afficher son contenu
            ImageObjet image = (ImageObjet)e.Data.GetData(typeof(ImageObjet));
            if (image != null)
            {
                Console.WriteLine("On a sélectionné une image.");
            }
        }

        // On ajoute l'objet dans la nouvelle ListBox et on le supprime de l'ancienne 
        private void ImageDropEvent(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            FileObjet data = (ImageObjet)e.Data.GetData(typeof(ImageObjet));
            
            if (data == null)
            {   // cas où c'est un album
                data = (AlbumObjet)e.Data.GetData(typeof(AlbumObjet));
                // On ne fait rien en fait, on n'est pas censé déplacer les albums
            }
            else
            {   // cas où c'est une image
                if ((dragSource.ItemsSource != parent.ItemsSource) && (currentAlbum != -1))
                { // si on déplace d'une zone à une autre on fait le déplacement, sinon on ne fait rien
                  // et de même on ne fait le déplacement que si on est en train de regarder des images d'un album
                    //On fait le changement dans les collections pour l'affichage graphique
                    ((IList)dragSource.ItemsSource).Remove(data);
                    ((IList)parent.ItemsSource).Add(data);
                    
                    //Et maintenant on s'occupe des données (téléchargement, upload dans la base)

                    //Si il s'agit d'une image distante qu'on déplace
                    if (((IList)dragSource.ItemsSource).Contains(retourAlbum))
                    {
                        //telecharger(data, cheminActuel);
                    }
                    //Sinon c'est une image locale qu'on upload
                    else
                    {
                        //uploader(data, currentAlbum);
                    }
                }
            }
        }

        // On récupére l'objet que que l'on a dropé 
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = (UIElement)source.InputHitTest(point);
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = (UIElement)VisualTreeHelper.GetParent(element);
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        } 
    }

    public class FileObjet
    {
        public byte[] Image { get; set; }
        public String Nom { get; set; }

        public FileObjet(String Nom)
        {
            this.Nom = Nom;
            this.Image = Image;
        }
    }

    public class ImageObjet : FileObjet
    {
        public String Id { get; set; }

        public ImageObjet(String Nom, byte[] Image, String Id) : base(Nom)
        {
            this.Image = Image;
            this.Id = Id;
        }
    }

    public class AlbumObjet : FileObjet
    {
        public int Id { get; set; }

        public AlbumObjet(String Nom, int Id) : base(Nom)
        {
            this.Nom = Nom;
            this.Image = ClientTest.Program.lireFichier(@"D:\dossier.gif");
            this.Id = Id;
        }
    }

    public class FileCollection : ObservableCollection<FileObjet>
    { }



    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
CultureInfo culture)
        {
            // crée un BitmapImage à partir d'un byte[] 
            BitmapImage imageSource = null;
            byte[] array = (byte[])value;
            if (array != null)
            {
                imageSource = new BitmapImage();
                imageSource.BeginInit();
                imageSource.StreamSource = new MemoryStream(array);
                imageSource.EndInit();
            }
            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object
parameter, CultureInfo culture)
        {
            throw new Exception("Non implementee");
        }
    }



    public class CreateAlbum : ICommand
    {

        public void Execute(object parameter)
        {
            MainViewModel vm = (MainViewModel)parameter;
            string album = vm.Album;

            MainWindow fenetre = new MainWindow("D:\\");
            fenetre.Show();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }


    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(string album)
        {
            this.Album = album;
            this.CreateAlbum = new CreateAlbum();
        }

        private string album;
        public string Album
        {
            get
            {
                return album;
            }
            set
            {
                album = value;
                this.OnPropertyChanged("Album");
            }
        }

        public ICommand CreateAlbum { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    
}
