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

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImageCollection imageCollection1, imageCollection2;

        public MainWindow()
        {
            InitializeComponent();

            // On crée notre collection d'image et on y ajoute deux images 
            imageCollection1 = new ImageCollection();
            imageCollection1.Add(new ImageObjet("celestial",
            ClientTest.Program.lireFichier(@"D:\celestial.jpg")));
            imageCollection1.Add(new ImageObjet("pearlscale",
            ClientTest.Program.lireFichier(@"D:\pearlscale.jpg")));

            // On parcourt le répertoire courant
            //WhileSystem.IO.Directory.GetDirectories("*.*"); //root.GetDirectories("*.*", System.IO.SearchOption.AllDirectories);
            imageCollection2 = new ImageCollection();
            imageCollection2.Add(new ImageObjet("celestial",
            ClientTest.Program.lireFichier(@"D:\celestial.jpg")));
            imageCollection2.Add(new ImageObjet("pearlscale",
            ClientTest.Program.lireFichier(@"D:\pearlscale.jpg")));

            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML 
            ObjectDataProvider imageSource =
            (ObjectDataProvider)FindResource("ImageCollection1");
            imageSource.ObjectInstance = imageCollection1;

            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML 
            ObjectDataProvider imageSource2 =
            (ObjectDataProvider)FindResource("ImageCollection2");
            imageSource2.ObjectInstance = imageCollection2;
        }


        ListBox dragSource = null;

        // On initie le Drag and Drop 
        private void ImageDragEvent(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));
            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        // On ajoute l'objet dans la nouvelle ListBox et on le supprime de l'ancienne 
        private void ImageDropEvent(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            ImageObjet data = (ImageObjet)e.Data.GetData(typeof(ImageObjet));
            ((IList)dragSource.ItemsSource).Remove(data);
            ((IList)parent.ItemsSource).Add(data);
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


    
}
