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
using System.Windows.Media.Animation;
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
            if (Properties.Settings.Default.hidden == false)
            {
                molkText.Opacity = 1;
                unmolkText.Opacity = 1;
            }
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


        private void leftRingMosueEnter(object sender, MouseEventArgs e)
        {

            LeftRing.Visibility = Visibility.Visible;
            Storyboard story = (Storyboard)FindResource("RingAnimation");
            LeftRing.BeginStoryboard(story);


        }

        private void leftRingMouseLeave(object sender, MouseEventArgs e)
        {
            LeftRing.Visibility = Visibility.Hidden;
        }

        private void rightRingMosueEnter(object sender, MouseEventArgs e)
        {
            RightRing.Visibility = Visibility.Visible;
            Storyboard story = (Storyboard)FindResource("RingAnimation2");
            RightRing.BeginStoryboard(story);

        }

        private void rightRingMouseLeave(object sender, MouseEventArgs e)
        {
            RightRing.Visibility = Visibility.Hidden;
        }


        private void exitApp(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void exitMouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard story = (Storyboard)FindResource("ExitButton");
            Exit.BeginStoryboard(story);
        }



        private void menuShow(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.hidden = !Properties.Settings.Default.hidden;
            if (Properties.Settings.Default.hidden == true)
            {
                molkText.Opacity = 0;
                unmolkText.Opacity = 0;
                exitText.Opacity = 0;
            }
            else
            {
                molkText.Opacity = 1;
                unmolkText.Opacity = 1;
                exitText.Opacity = 1;
            }

        }
    }
}
