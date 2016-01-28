using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls;
using Prism.Regions;
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

                RegisterShellViews(regionManager);
            }
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
