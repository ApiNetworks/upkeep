using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upkeep.Shell.Infrastructure.Constatnts;
using Upkeep.Shell.Infrastructure.Base;
using Upkeep.Modules.FileExplorer.Views;

namespace Upkeep.Modules.FileExplorer
{
    public class FileExplorerModule : BaseModule
    {
        public FileExplorerModule(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {
            Initialize();
        }
        public new void Initialize()
        {
            IRegion fileRegion = RegionManager.Regions[RegionNames.FileExplorerRegionName];
            var fileView = UnityContainer.Resolve<FileExplorerView>();
            fileRegion.Add(fileView, typeof(FileExplorerView).FullName);

            //var fileView = UnityContainer.Resolve<FileExplorerView>();
            //fileRegion.Add(fileView, typeof(FileExplorerView).FullName);

            fileRegion.Activate(fileView);
        }
    }
}
