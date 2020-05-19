﻿using System;
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
                    items.Add(Path.GetFileName(file), Path.GetFullPath(file));
                }
                foreach (string dir in _directory)
                {
                    listFiles.Items.Add(Path.GetFileName(dir));
                    items.Add(Path.GetFileName(dir), Path.GetFullPath(dir));
                }

            }
        }

        private void homePage(object sender, RoutedEventArgs e)
        {
            mainWindow.GoToHomePage();
        }
        bool hidden = true;
        private void molk_show(object sender, RoutedEventArgs e)
        {
            hidden = !hidden;
            if (hidden == true)
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
