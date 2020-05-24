using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using WinForms = System.Windows.Forms;
using TextBox = System.Windows.Controls.TextBox;
using ToolTip = System.Windows.Controls.ToolTip;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace MolkZip
{
    /// <summary>
    /// Interaction logic for UnMolkPage.xaml
    /// </summary>
    public partial class UnMolkPage : Page
    {
        private MainWindow mainWindow;
        private System.Windows.Forms.Timer timer2;
        private int counter = 10;

        public UnMolkPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            if (Properties.Settings.Default.hidden == false)
            {
                unmolkText.Opacity = 1;
                browseText.Opacity = 1;
                arrowText.Opacity = 1;
                exitText.Opacity = 1;
                addFiles.Opacity = 1;
                removeFiles.Opacity = 1;
            }
        }


        private void PreviousPageClick(object sender, RoutedEventArgs e)
        {
            mainWindow.GoToHomePage();
        }

        private void GetPath(object sender, RoutedEventArgs e)
        {
            OpenFileDialog filepath = new OpenFileDialog();
            filepath.Filter = "Molk Files|*.molk";
            DialogResult result = filepath.ShowDialog();
            Path_Name.Text = filepath.FileName;
            //It works! I don't know how and i don't question it!

            if (result == WinForms.DialogResult.OK)
            {
                listFiles.Items.Clear();
                Process proc = new Process();

                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;

                string command = "unmolk -l " + filepath.FileName + " > temp.txt";
                bool once = true;
                Regex reg = new Regex(@"(\:+\d{2}\s{3})", RegexOptions.Compiled);
                Regex line = new Regex(@"(\r\n)");
                string text = System.IO.File.ReadAllText("temp.txt");
                MatchCollection matches = reg.Matches(text);
                MatchCollection linematch = line.Matches(text);
                foreach (Match mat in matches)
                {

                    GroupCollection groups = mat.Groups;
                    int closLin = 0;

                    foreach (Match lin in linematch)
                    {
                        if (lin.Index > groups[1].Index)
                        {
                            closLin = lin.Index;
                            listFiles.Items.Add(text.Substring(groups[1].Index + 6, (closLin - groups[1].Index - 6)));
                            break;
                        }
                    }
                }
                while (once)
                {

                    proc.Start();
                    proc.StandardInput.WriteLine($"{command}");
                    proc.StandardInput.Flush();
                    proc.StandardInput.Close();

                    proc.WaitForExit();
                    once = false;
                }
            }
        }

        private void UnMolkShow(object sender, RoutedEventArgs e)
        {
            //Method for viewing help texts by clicking the pen
            Properties.Settings.Default.hidden = !Properties.Settings.Default.hidden;
            if (Properties.Settings.Default.hidden == true)
            {
                unmolkText.Opacity = 0;
                browseText.Opacity = 0;
                arrowText.Opacity = 0;
                exitText.Opacity = 0;
                removeFiles.Opacity = 0;
                addFiles.Opacity = 0;
            }
            else
            {
                unmolkText.Opacity = 1;
                browseText.Opacity = 1;
                arrowText.Opacity = 1;
                exitText.Opacity = 1;
                removeFiles.Opacity = 1;
                addFiles.Opacity = 1;
            }

        }

        private void ExitApp3(object sender, RoutedEventArgs e)
        {
            //Turns off the app
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void ExitMouseEnter3(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Animation for exit button
            Storyboard story = (Storyboard)FindResource("ExitButton3");
            Exit3.BeginStoryboard(story);

        }

        private void RemoveFiles(object sender, RoutedEventArgs e)
        {
            //remove files from right listBox
            List<string> files = new List<string>();
            try
            {
                foreach (string item in chosenFiles.SelectedItems)
                {
                    files.Add(item);
                }

                for (int i = 0; i < chosenFiles.SelectedItems.Count + 1; i++)
                {
                    chosenFiles.Items.Remove(files[i]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No files have been chosen. Select your files that you want to remove first. ", "Invalid operation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddFiles(object sender, RoutedEventArgs e)
        {
            //Add files to right listBox from left listBox
            foreach (string item in listFiles.SelectedItems)
            {
                for (int i = 0; i < chosenFiles.Items.Count + 1; i++)
                {
                    if (!chosenFiles.Items.Contains(item))
                    {
                        chosenFiles.Items.Add(item);
                    }
                }
            }
        }

        private void UnMolk(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog target = new FolderBrowserDialog();
            target.ShowNewFolderButton = false;
            target.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            target.ShowDialog();
            Process proc = new Process();

            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;

            string command = "unmolk -o " + Path_Name.Text + " ";
            foreach (string item in chosenFiles.Items)
            {
                command += item + " ";
            }
            bool once = true;
            while (once)
            {

                proc.Start();
                proc.StandardInput.WriteLine($"{command}-d " + target.SelectedPath);
                proc.StandardInput.Flush();
                proc.StandardInput.Close();

                proc.WaitForExit();
                once = false;
            }
            progress2.Visibility = Visibility.Visible;
            Progress();
        }

        private void Progress()
        {

            timer2 = new System.Windows.Forms.Timer();
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Interval = 1000; // 1 second
            timer2.Start();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            counter--;
            if (counter == 0)
                timer2.Stop();
            progress2.Visibility = Visibility.Hidden;

        }

        private void SelectAllExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //CTRL + A for listBoxes
            listFiles.SelectAll();
            chosenFiles.SelectAll();
        }
    }
}
