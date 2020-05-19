using System;
using System.Windows;
using System.Windows.Controls;
using WinForms = System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Path = System.IO.Path;

namespace MolkZip
{
    /// <summary>
    /// Interaction logic for MolkPage.xaml
    /// </summary>
    public partial class MolkPage : Page
    {
        private MainWindow mainWindow;

        public MolkPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            if (Properties.Settings.Default.hidden == false)
            {
                molk_Text.Opacity = 1;
                Browse_Text.Opacity = 1;
                Arrow_Text.Opacity = 1;
            }
        }


        public Dictionary<string, string> items = new Dictionary<string, string>();
        private void browseFolder(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                //----< Selected Folder >----
                //< Selected Path >
                String sPath = folderDialog.SelectedPath;
                folderName.Text = sPath;
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

        private void homePage(object sender, RoutedEventArgs e)
        {
            mainWindow.GoToHomePage();
        }

        private void molk_show(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.hidden = !Properties.Settings.Default.hidden;
            if (Properties.Settings.Default.hidden == true)
            {
                molk_Text.Opacity = 0;
                Browse_Text.Opacity = 0;
                Arrow_Text.Opacity = 0;
            }
            else
            {
                molk_Text.Opacity = 1;
                Browse_Text.Opacity = 1;
                Arrow_Text.Opacity = 1;
            }

        }

        // Loopar genom allt i listan och om den redan finns så skippar den att lägga till den
        private void select_files(object sender, RoutedEventArgs e)
        {
             foreach(string item in listFiles.SelectedItems)
             {
                for (int i = 0; i < Choosen_files.Items.Count+1; i++)
                {
                    if (!Choosen_files.Items.Contains(item))
                    {
                        Choosen_files.Items.Add(item);
                        items.Add(item, folderName.Text + "\\" + item);
                    }
                }
             }
        }

        private void Molk(object sender, RoutedEventArgs e)
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
            foreach(KeyValuePair<string, string> entry in items)
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
        }

        private void remove_files(object sender, RoutedEventArgs e)
        {
            List<string> files = new List<string>();
            foreach (string item in Choosen_files.SelectedItems)
            {
                files.Add(item);
            }
            for (int i = 0; i < Choosen_files.SelectedItems.Count+1; i++)
            {
                items.Remove(files[i]);
                Choosen_files.Items.Remove(files[i]);
            }
        }
    }
}
