using MahApps.Metro.Controls;
using Prism.Regions;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Upkeep.Shell.Infrastructure.Constatnts;
using Upkeep.Shell.Views;

namespace Upkeep.Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : MetroWindow
    {
        public Shell(IRegionManager regionManager)
        {
            InitializeComponent();

            if (regionManager != null)
            {
                SetRegionManager(regionManager, this.SettingsRegionContentControl, RegionNames.SettingsRegionName);
                SetRegionManager(regionManager, this.AboutRegionContentControl, RegionNames.AboutRegionName);
                SetRegionManager(regionManager, this.HelpRegionContentControl, RegionNames.HelpRegionName);
                SetRegionManager(regionManager, this.TabRegionContentControl, RegionNames.TabRegionName);
                SetRegionManager(regionManager, this.QueryRegionContentControl, RegionNames.QueryRegionName);
                SetRegionManager(regionManager, this.FileExplorerRegionContentControl, RegionNames.FileExplorerRegionName);

                RegisterShellViews(regionManager);
            }

            //this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //foreach (string s in Directory.GetLogicalDrives())
            //{
            //    TreeViewItem item = new TreeViewItem();
            //    item.Header = s;
            //    item.Tag = s;
            //    item.FontWeight = FontWeights.Normal;
            //    item.Items.Add(dummyNode);
            //    item.Expanded += new RoutedEventHandler(folder_Expanded);
            //    foldersItem.Items.Add(item);
            //}
        }

        private void RegisterShellViews(IRegionManager regionManager)
        {
            IRegion aboutRegion = regionManager.Regions[RegionNames.AboutRegionName];
            if (aboutRegion != null)
            {
                AboutView aboutView = new AboutView();
                aboutRegion.Add(aboutView);
                aboutRegion.Activate(aboutView);
            }
        }

        void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }

        public string SelectedImagePath { get; set; }

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
            System.Windows.MessageBox.Show(SelectedImagePath);
        }

        private object dummyNode = null;
    }
}
