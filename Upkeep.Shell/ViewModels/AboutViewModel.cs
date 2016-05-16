using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Upkeep.Shell.Infrastructure.Base;
using Upkeep.Shell.Models;

namespace Upkeep.Shell.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ObservableCollection<string> RegionNames { get; private set; }

        public ObservableCollection<RegionInfo> Regions { get; private set; }

        public AboutViewModel() 
        {
            RegionNames = new ObservableCollection<string>(GetAvailableRegionNames());
            Regions = new ObservableCollection<RegionInfo>(GetRegionInfoModels());
        }

        private List<string> GetAvailableRegionNames()
        {
            return (from item in this.RegionManager.Regions select item.Name).ToList<string>();
        }

        // TODO:
        private void GetRegisteredViews()
        {
            List<string> regions = GetAvailableRegionNames();
            foreach (string regionName in regions)
            {
                IRegion reg = RegionManager.Regions[regionName];
                IViewsCollection col = reg.Views;
            }
        }

        private List<RegionInfo> GetRegionInfoModels()
        {
            List<RegionInfo> regions = new List<RegionInfo>();
            foreach (IRegion region in this.RegionManager.Regions)
            {
                RegionInfo regionModel = new RegionInfo();
                regionModel.RegionName = region.Name;
                regionModel.Views = new ObservableCollection<ViewInfo>();
                for (int v=0; v<5; v++)
                {
                    ViewInfo m = new ViewInfo();
                    m.ViewName = v.ToString();
                    regionModel.Views.Add(m);
                }
                regions.Add(regionModel);
            }
            return regions;
        }

        public List<TreeViewItem> RegionTreeViewItem
        {
            get
            {
                List<TreeViewItem> items = new List<TreeViewItem>();
                foreach (var region in this.RegionManager.Regions)
                {
                    TreeViewItem tItem = new TreeViewItem();
                    tItem.Header = region.Name;

                    TreeViewItem vItem = new TreeViewItem();
                    foreach (IViewsCollection view in region.Views)
                    {
                        vItem.Header = view.ToString();
                        tItem.Items.Add(vItem);
                    }
                    items.Add(tItem);
                }

                return items;
            }
        }
    }

    public class RegionInfo
    {
        public string RegionName { get; set; }

        public ObservableCollection<ViewInfo> Views { get; set; }
    }

    public class ViewInfo
    {
        public string ViewName { get; set; }
    }
}
