using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upkeep.Modules.DatabaseExplorer.Views;
using Upkeep.Shell.Infrastructure.Base;

namespace Upkeep.Modules.DatabaseExplorer
{
    public class DbExplorerModule : BaseModule
    {
        public DbExplorerModule(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {
            Initialize();
        }
        public new void Initialize()
        {
            IRegion tabRegion = RegionManager.Regions[Upkeep.Shell.Infrastructure.Constatnts.RegionNames.TabRegionName];
            var tabView = UnityContainer.Resolve<Views.TableExplorerView>();
            tabRegion.Add(tabView, typeof(TableExplorerView).FullName);
            tabRegion.Activate(tabView);

            IRegion queryRegion = RegionManager.Regions[Upkeep.Shell.Infrastructure.Constatnts.RegionNames.QueryRegionName];
            var queryView = UnityContainer.Resolve<Views.QueryExplorerView>();
            queryRegion.Add(queryView, typeof(QueryExplorerView).FullName);
            queryRegion.Activate(queryView);

            //RegionManager.RegisterViewWithRegion(Upkeep.Shell.Infrastructure.Constatnts.RegionNames.TabRegionName, typeof(Views.TableExplorerView));
            //RegionManager.Regions["WorkspaceRegion"].Add(new Views.TableExplorerView(), "TableExplorerView");

            ////RegionManager.RequestNavigate(Upkeep.Shell.Infrastructure.Constatnts.RegionNames.TabRegionName, "TableExplorerView");

            ////RegionManager.RegisterViewWithRegion(Upkeep.Shell.Infrastructure.Constatnts.RegionNames.TabRegionName, typeof(Views.TableExplorerView));
            ////RegionManager.RequestNavigate(Upkeep.Shell.Infrastructure.Constatnts.RegionNames.TabRegionName, "TableExplorerView");

            //TableExplorerView view = new TableExplorerView();


            //IRegion mainRegion = base.RegionManager.Regions[Upkeep.Shell.Infrastructure.Constatnts.RegionNames.TabRegionName];

            //mainRegion.GetView("TableExplorerView");
            //mainRegion.Activate(view);
        }
    }

}
