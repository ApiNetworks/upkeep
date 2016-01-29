using System.Collections.ObjectModel;

namespace Upkeep.Shell.Models
{
    public class RegionInfoModel
    {
        public string RegionName { get; set; }

        public ObservableCollection<ViewInfoModel> ViewInfoModels { get; set; }
    }
}
