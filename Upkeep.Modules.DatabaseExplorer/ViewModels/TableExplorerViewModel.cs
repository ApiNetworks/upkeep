using Newtonsoft.Json;
using Prism.Commands;
using RxDM.Entities.Mapper;
using RxDM.Entities.Queries;
using RxDM.Entities.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Upkeep.Shell.Infrastructure.Base;

namespace Upkeep.Modules.DatabaseExplorer.ViewModels
{
    public class TableExplorerViewModel : BaseViewModel
    {
        public ObservableCollection<TableInfo> TableInformation { get; private set; }

        public ObservableCollection<ColumnInfo> ColumnInformation { get; private set; }

        public ObservableCollection<EntityInfo> EntityInformation { get; private set; }

        public TableExplorerViewModel()
        {
            this.DataGridDoubleClick = new DelegateCommand<object>(this.OnDataGridDoubleClick);

            LoadTableInformation();
            LoadColumnInformation();
            LoadEntityInformation();
        }

        private string _selectedSchema;
        public string SelectedSchema
        {
            get { return _selectedSchema; }
            set { SetProperty(ref this._selectedSchema, value); }
        }

        private EntityInfo _selectedEntity;
        public EntityInfo SelectedEntity
        {
            get { return _selectedEntity; }
            set { SetProperty(ref this._selectedEntity, value); }
        }

        public ICommand DataGridDoubleClick { get; private set; }
        private void OnDataGridDoubleClick(object arg)
        {
            if (SelectedEntity != null)
            {
                var recordSchema = SelectedEntity.Schema;
                SelectedSchema = JsonConvert.SerializeObject(recordSchema, Formatting.Indented);
            }
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

        public void LoadEntityInformation()
        {
            Eif2Mapper mapper = new Eif2Mapper();
            List<EntityInfo> results = mapper.GetEntityInformation();
            EntityInformation = null;
            EntityInformation = new ObservableCollection<EntityInfo>(results);
        }
    }
}
