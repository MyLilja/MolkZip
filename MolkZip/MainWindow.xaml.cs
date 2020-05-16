using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MolkZip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //// Create an Image element.
            //Image croppedImage = new Image();
            //croppedImage.Width = 1280;
            //croppedImage.Margin = new Thickness(5);

            //// Create a CroppedBitmap based off of a xaml defined resource.
            //CroppedBitmap cb = new CroppedBitmap(
            //   (BitmapSource)this.Resources["RedBrick"],
            //   new Int32Rect(0, 0, 720, 1280));       //select region rect
            //croppedImage.Source = cb;                 //set image source to cropped
        }

       

        private void OpenMolkWindow(object sender, RoutedEventArgs e)
        {
            MolkWindow molk= new MolkWindow();
            molk.Show();
            this.Close();
            
        }

        private void OpenUnmolkWindow(object sender, RoutedEventArgs e)
        {
            UnmolkWindow unMolk = new UnmolkWindow();       
            unMolk.Show();
            this.Close();
        }
    }
}
