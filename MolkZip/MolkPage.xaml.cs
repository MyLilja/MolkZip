﻿using System;
using System.Windows;
using System.Windows.Controls;
using WinForms = System.Windows.Forms;
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

    }
}
