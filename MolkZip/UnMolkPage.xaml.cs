using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Animation;
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

        private void exitApp3(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void exitMouseEnter3(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Storyboard story = (Storyboard)FindResource("ExitButton3");
            Exit3.BeginStoryboard(story);
        }
    }
}
