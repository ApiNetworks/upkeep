using RxDM.Entities.Queries;
using System.Collections.ObjectModel;
using Upkeep.Shell.Infrastructure.Base;

namespace Upkeep.Modules.DatabaseExpolorer.ViewModels
{
    public class TableExplorerViewModel : BaseViewModel
    {
        public ObservableCollection<TableInfo> TableInformation { get; private set; }

        public ObservableCollection<ColumnInfo> ColumnInformation { get; private set; }

        public TableExplorerViewModel()
        {
            LoadTableInformation();
            LoadColumnInformation();
        }

        public void LoadTableInformation()
        {
            var results = QueryHelper.GetDatabaseTableInfo();
            TableInformation = new ObservableCollection<TableInfo>(results);
        }

        public void LoadColumnInformation()
        {
            var results = QueryHelper.GetDatabaseTableColumnInfo();
            ColumnInformation = new ObservableCollection<ColumnInfo>(results);
        }
    }
}
