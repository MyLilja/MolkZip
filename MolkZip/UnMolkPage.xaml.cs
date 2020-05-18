using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using TextBox = System.Windows.Controls.TextBox;
using ToolTip = System.Windows.Controls.ToolTip;

namespace MolkZip
{
    /// <summary>
    /// Interaction logic for UnMolkPage.xaml
    /// </summary>
    public partial class UnMolkPage : Page
    {
        private MainWindow mainWindow;

        public UnMolkPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }


        private void previousPage_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.GoToHomePage();
        }

        private void GetPath(object sender, RoutedEventArgs e)
        {
            OpenFileDialog filepath = new OpenFileDialog();
            filepath.Filter = "Molk Files|*.molk";
            filepath.ShowDialog();
            Path_Name.Text = filepath.FileName;
        }
        bool hidden = true;
        private void Unmolk_show(object sender, RoutedEventArgs e)
        {
            hidden = !hidden;
            if(hidden == true)
            {
                Unmolk_Text.Opacity = 0;
                Browse_Text.Opacity = 0;
                Arrow_Text.Opacity = 0;
            }
            else
            {
                Unmolk_Text.Opacity = 1;
                Browse_Text.Opacity = 1;
                Arrow_Text.Opacity = 1;
            }

        }
    }
}
