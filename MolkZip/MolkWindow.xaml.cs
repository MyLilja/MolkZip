using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using WinForms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Path = System.IO.Path;

namespace MolkZip
{
    /// <summary>
    /// Interaction logic for MolkWindow.xaml
    /// </summary>
    public partial class MolkWindow : Window
    {
        

        public MolkWindow()
        {
            InitializeComponent();
            
        }

        private void selectFolder(object sender, RoutedEventArgs e)
        {

            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            DialogResult result = folderDialog.ShowDialog();

            //WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            //folderDialog.ShowNewFolderButton = false;
            //folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            //WinForms.DialogResult result = folderDialog.ShowDialog();

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

        private void previousPage(object sender, RoutedEventArgs e)
        {
            MainWindow homePage = new MainWindow();
            homePage.Show();
            this.Close();
        }
    }
}
