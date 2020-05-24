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
        #region Fields

        private object content;
        #endregion

        #region Initialize

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
        #endregion

        #region Methods

        public void GoToHomePage()
        {
            //Marks this page as homepage
            Content = content;
        }

        private void OpenMolkPage(object sender, RoutedEventArgs e)
        {
            //Opens up the Molk Page
            MolkPage molk = new MolkPage(this);
            this.Content = molk;
        }



        private void OpenUnMolkPage(object sender, RoutedEventArgs e)
        {
            //Opens up the UnMolk Page
            UnMolkPage unMolk = new UnMolkPage(this);
            this.Content = unMolk;
        }


        private void LeftRingMosueEnter(object sender, MouseEventArgs e)
        {
            //Animation of left ring
            LeftRing.Visibility = Visibility.Visible;
            Storyboard story = (Storyboard)FindResource("RingAnimation");
            LeftRing.BeginStoryboard(story);
        }

        private void LeftRingMouseLeave(object sender, MouseEventArgs e)
        {
            //Hides animation of left ring
            LeftRing.Visibility = Visibility.Hidden;
        }

        private void RightRingMosueEnter(object sender, MouseEventArgs e)
        {
            //Animation of right ring
            RightRing.Visibility = Visibility.Visible;
            Storyboard story = (Storyboard)FindResource("RingAnimation2");
            RightRing.BeginStoryboard(story);

        }

        private void RightRingMouseLeave(object sender, MouseEventArgs e)
        {
            //Hides animation of right ring
            RightRing.Visibility = Visibility.Hidden;
        }


        private void ExitApp(object sender, RoutedEventArgs e)
        {
            //Turns off the app
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void ExitMouseEnter(object sender, MouseEventArgs e)
        {
            //Animation for exit button
            Storyboard story = (Storyboard)FindResource("ExitButton");
            Exit.BeginStoryboard(story);
        }



        private void MenuShow(object sender, RoutedEventArgs e)
        {
            //Method for viewing help texts by clicking the pen
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
        #endregion
    }
}
