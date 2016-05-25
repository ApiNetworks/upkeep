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
                SetRegionManager(regionManager, this.DatabaseRegionContentControl, RegionNames.DatabaseRegionName);

                RegisterShellViews(regionManager);
            }

            this.Loaded += new RoutedEventHandler(ShellWindow_Loaded);
        }

        private void ShellWindow_Loaded(object sender, RoutedEventArgs e)
        {

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
    }
}
