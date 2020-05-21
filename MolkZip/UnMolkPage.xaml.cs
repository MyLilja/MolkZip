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
            if (Properties.Settings.Default.hidden == false)
            {
                unmolkText.Opacity = 1;
                browseText.Opacity = 1;
                arrowText.Opacity = 1;
                exitText.Opacity = 1;
            }
        }


        private void previousPage_Click(object sender, RoutedEventArgs e)
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
                foreach(Match mat in matches)
                {

                    GroupCollection groups = mat.Groups;
                    int closLin = 0;

                    foreach (Match lin in linematch)
                    {
                        if (lin.Index > groups[1].Index)
                        {
                            closLin = lin.Index;
                            listFiles.Items.Add(text.Substring(groups[1].Index+6, (closLin - groups[1].Index-6)));
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
        private void Unmolk_show(object sender, RoutedEventArgs e)
        {
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

        private void exitApp3(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void exitMouseEnter3(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Storyboard story = (Storyboard)FindResource("ExitButton3");
            Exit3.BeginStoryboard(story);

        }

        private void remove_files(object sender, RoutedEventArgs e)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (string item in Choosen_files.SelectedItems)
                {
                    files.Add(item);
                }

                for (int i = 0; i < Choosen_files.SelectedItems.Count + 1; i++)
                {
                    Choosen_files.Items.Remove(files[i]);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Invalid Operation. " + ex.Message, "No files to remove from this list.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void add_files(object sender, RoutedEventArgs e)
        {
            foreach (string item in listFiles.SelectedItems)
            {
                for (int i = 0; i < Choosen_files.Items.Count + 1; i++)
                {
                    if (!Choosen_files.Items.Contains(item))
                    {
                        Choosen_files.Items.Add(item);
                    }
                }
            }
        }

         private void Unmolk(object sender, RoutedEventArgs e)
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
             foreach (string item in Choosen_files.Items)
             {
                 command += item + " ";
             }
             bool once = true;
             while (once)
             {

                 proc.Start();
                WinForms.MessageBox.Show($"{command}-d " + target.SelectedPath);
                 proc.StandardInput.WriteLine($"{command}-d " + target.SelectedPath);
                 proc.StandardInput.Flush();
                 proc.StandardInput.Close();

                 proc.WaitForExit();
                 once = false;
             }
         }
    }
}
