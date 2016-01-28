using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Upkeep.Shell.Infrastructure.Base;

namespace Upkeep.Shell.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ObservableCollection<string> RegionNames { get; private set; }

        public AboutViewModel() 
        {
            RegionNames = new ObservableCollection<string>(GetAvailableRegionNames());
        }

        private List<string> GetAvailableRegionNames()
        {
            return (from item in this.RegionManager.Regions select item.Name).ToList<string>();
        }

        private List<string> GetAvailableViewNames(string regionName)
        {
            //if(this.RegionManager.Regions.ContainsRegionWithName[regionName])
            //{ }

            return (from item in this.RegionManager.Regions select item.Name).ToList<string>();
        }
    }
}
