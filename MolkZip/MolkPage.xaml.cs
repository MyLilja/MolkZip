using System;
using System.Windows;
using System.Windows.Controls;
using WinForms = System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Path = System.IO.Path;
using System.Windows.Media.Animation;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Input;
using ProgressBar = System.Windows.Controls.ProgressBar;
using Orientation = System.Windows.Controls.Orientation;
using System.Windows.Threading;
using Application = System.Windows.Forms.Application;

namespace MolkZip
{
    /// <summary>
    /// Interaction logic for MolkPage.xaml
    /// </summary>
    public partial class MolkPage : Page
    {
        #region Fields

        private MainWindow mainWindow;
        private string pathName;
        public Dictionary<string, string> items = new Dictionary<string, string>();
        private System.Windows.Forms.Timer timer1;
        private int counter = 6;
        #endregion  

        #region Initialize

        public MolkPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            if (Properties.Settings.Default.hidden == false)
            {
                molkText.Opacity = 1;
                browseText.Opacity = 1;
                arrowText.Opacity = 1;
                exitText.Opacity = 1;
                addFiles.Opacity = 1;
                removeFiles.Opacity = 1;
            }
        }
        #endregion

        #region Methods

        private void BrowseFiles(object sender, RoutedEventArgs e)
        {
            //Opens up a folder dialog and displays the files in a listBox
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            DialogResult result = folderDialog.ShowDialog();


            if (result == WinForms.DialogResult.OK)
            {
                // ----< Selected Folder > ----
                //< Selected Path >
                String sPath = folderDialog.SelectedPath;
                folderName.Text = sPath;
                pathName = folderName.Text;
                //</ Selected Path >

                //--------< Folder >--------
                DirectoryInfo folder = new DirectoryInfo(sPath);
                if (folder.Exists)
                {
                    //------< @Loop: Files >------
                    foreach (FileInfo fileInfo in folder.GetFiles())
                    {
                        //----< File >----
                        String sDate = fileInfo.CreationTime.ToString("yyyy-MM-dd");
                        Debug.WriteLine("#Debug: File: " + fileInfo.Name + " Date:" + sDate);
                    }
                }

                listFiles.Items.Clear();

                string[] files = Directory.GetFiles(folderDialog.SelectedPath);
                string[] _directory = Directory.GetDirectories(folderDialog.SelectedPath);

                foreach (string file in files)
                {
                    listFiles.Items.Add(Path.GetFileName(file));
                }
                foreach (string dir in _directory)
                {
                    listFiles.Items.Add(Path.GetFileName(dir));
                }

            }
        }

        private void PreviousPageClick(object sender, RoutedEventArgs e)
        {
            //back to main menu
            mainWindow.GoToHomePage();
        }


        private void ExitApp2(object sender, RoutedEventArgs e)
        {
            //Turns off the app
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void ExitMouseEnter2(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Animation for exit button
            Storyboard story = (Storyboard)FindResource("ExitButton2");
            Exit2.BeginStoryboard(story);
        }




        private void MolkFiles(object sender, RoutedEventArgs e)
        {            
            SaveFileDialog target = new SaveFileDialog();
            target.Filter = "Molk|*.molk";
            target.Title = "Save Molk file";
            target.ShowDialog();

            Process proc = new Process();

            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;

            string command = "molk -j " + target.FileName + " ";
            foreach (KeyValuePair<string, string> entry in items)
            {
                command += '"' + entry.Value + '"' + " ";
            }
            bool once = true;
            while (once)
            {

                proc.Start();
                proc.StandardInput.WriteLine($"{command}");
                proc.StandardInput.Flush();
                proc.StandardInput.Close();
                proc.WaitForExit();
                once = false;
            }
            progress.Visibility = Visibility.Visible;
            Progress();          
        }

        private void Progress(int time = 1000)
        {
            //UX design, giving the illusion of progress loading
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = time; // 1 second
            timer1.Start();
           
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Second part of progress duration
            counter--;
            if (counter == 0)
                timer1.Stop();
                progress.Visibility = Visibility.Hidden;
            
        }

        

        // Loopar genom allt i listan och om den redan finns så skippar den att lägga till den
        private void AddFiles(object sender, RoutedEventArgs e)
        {
            //Add items from left listBox to right listBox
            foreach (string item in listFiles.SelectedItems)
            {
                for (int i = 0; i < chosenFiles.Items.Count + 1; i++)
                {
                    if (!chosenFiles.Items.Contains(item))
                    {
                        chosenFiles.Items.Add(item);
                        items.Add(item, folderName.Text + "\\" + item);
                    }
                }
            }
        }

        private void RemoveFiles(object sender, RoutedEventArgs e)
        {
            //Remove items from right listBox
            List<string> files = new List<string>();

            foreach (string item in chosenFiles.SelectedItems)
            {
                files.Add(item);
            }

            try
            {

                for (int i = 0; i < chosenFiles.SelectedItems.Count + 1; i++)
                {
                    items.Remove(files[i]);
                    chosenFiles.Items.Remove(files[i]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No files have been chosen. Select your files that you want to remove first. ", "Invalid operation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowMolk(object sender, RoutedEventArgs e)
        {
            //Method for viewing help texts by clicking the pen
            Properties.Settings.Default.hidden = !Properties.Settings.Default.hidden;
            if (Properties.Settings.Default.hidden == true)
            {
                molkText.Opacity = 0;
                browseText.Opacity = 0;
                arrowText.Opacity = 0;
                exitText.Opacity = 0;
                addFiles.Opacity = 0;
                removeFiles.Opacity = 0;
            }
            else
            {
                molkText.Opacity = 1;
                browseText.Opacity = 1;
                arrowText.Opacity = 1;
                exitText.Opacity = 1;
                addFiles.Opacity = 1;
                removeFiles.Opacity = 1;
            }
        }

        private void SelectAllExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //CTRL + A for selecting all files in listBoxes
            listFiles.SelectAll();
            chosenFiles.SelectAll();
        }

        #endregion
    }
}
