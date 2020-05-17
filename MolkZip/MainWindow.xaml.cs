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
        private object content;

        public MainWindow()
        {
            InitializeComponent();
            content = Content;         
        }

        public void GoToHomePage()
        {
            Content = content;
        }

        private void openMolkPage(object sender, RoutedEventArgs e)
        {
            MolkPage molk = new MolkPage(this);
            this.Content = molk;
        }

       

        private void openUnMolkPage(object sender, RoutedEventArgs e)
        {
            UnMolkPage unMolk = new UnMolkPage(this);
            this.Content = unMolk;
        }
    }
}
