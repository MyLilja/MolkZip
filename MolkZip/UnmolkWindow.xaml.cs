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
using System.Windows.Shapes;

namespace MolkZip
{
    /// <summary>
    /// Interaction logic for UnmolkWindow.xaml
    /// </summary>
    public partial class UnmolkWindow : Window
    {
        public UnmolkWindow()
        {
            InitializeComponent();
        }

        private void _previousPage(object sender, RoutedEventArgs e)
        {
            MainWindow homePage = new MainWindow();
            homePage.Show();
            this.Close();
        }
    }
}
