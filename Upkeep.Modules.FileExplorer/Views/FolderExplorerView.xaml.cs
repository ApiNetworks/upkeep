﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Upkeep.Modules.FileExplorer.Controls;
using Upkeep.Modules.FileExplorer.Models;
using Upkeep.Modules.FileExplorer.ViewModels;

namespace Upkeep.Modules.FileExplorer.Views
{
    /// <summary>
    /// Interaction logic for FileExplorerView.xaml
    /// </summary>
    public partial class FileExplorerView : UserControl
    {
        private object dummyNode = null;
        FolderExplorerViewModel viewModel;

        public FileExplorerView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ControlWindow_Loaded);

            viewModel = new FolderExplorerViewModel();
            this.DataContext = viewModel;
        }

        private void ControlWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string s in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                foldersItem.Items.Add(item);
            }
        }

        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }

        private void foldersItem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            System.Windows.Controls.TreeView tree = (System.Windows.Controls.TreeView)sender;
            TreeViewItem temp = ((TreeViewItem)tree.SelectedItem);

            if (temp == null)
                return;
            SelectedImagePath = "";
            string temp1 = "";
            string temp2 = "";
            while (true)
            {
                temp1 = temp.Header.ToString();
                if (temp1.Contains(@"\"))
                {
                    temp2 = "";
                }
                SelectedImagePath = temp1 + temp2 + SelectedImagePath;
                if (temp.Parent.GetType().Equals(typeof(System.Windows.Controls.TreeView)))
                {
                    break;
                }
                temp = ((TreeViewItem)temp.Parent);
                temp2 = @"\";
            }
            //show user selected path

            string fileName = System.IO.Path.GetFileNameWithoutExtension(SelectedImagePath);
            PopulateFileList();
        }

        public string SelectedImagePath { get; private set; }

        public void PopulateFileList()
        {
            if (!String.IsNullOrEmpty(SelectedImagePath) && Directory.Exists(SelectedImagePath))
            {
                foreach (string filePath in Directory.GetFiles(SelectedImagePath, "*.eif2"))
                {
                    FileInfo file = new FileInfo(filePath);
                    FileInfoModel fileModel = new FileInfoModel
                    {
                        FullName = file.FullName,
                        Directory = file.DirectoryName,
                        Extension = file.Extension,
                        Length = file.Length,
                        Name = file.Name
                    };

                    if (viewModel.SelectedFiles.Any(x => x.FullName.Equals(fileModel.FullName)) == false)
                    {
                        viewModel.SelectedFiles.Add(fileModel);

                        // Run upload
                        fileModel.StartAsync();
                    }

                    this.Btn_ClearFiles.IsEnabled = true;
                    this.Btn_LoadFiles.IsEnabled = true;
                }
            }
        }

        private void Click_LoadFiles(object sender, RoutedEventArgs e)
        {

        }

        private void Click_ClearFiles(object sender, RoutedEventArgs e)
        {
            this.viewModel.SelectedFiles.Clear();
            this.Btn_ClearFiles.IsEnabled = false;
            this.Btn_LoadFiles.IsEnabled = false;
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var file = ((ListViewItem)sender).Content as FileInfoModel; //Casting back to the binded Track

            //if(file != null && file.Exists)
            //{
            //    var records = ReadLines(file, 100);
            //}

            this.FilePreviewWindow.IsOpen = true;

            //await this.ShowChildWindowAsync(new PreviewWindow() { IsModal = true, AllowMove=true });
        }

        private List<string> ReadLines(FileInfo file, int numberOfLines)
        {
            return File.ReadLines(file.FullName).Take(numberOfLines).ToList<string>();
        }
    }
}

