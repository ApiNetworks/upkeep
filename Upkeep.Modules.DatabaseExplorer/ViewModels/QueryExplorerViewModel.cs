using Prism.Commands;
using RxDM.Entities.Queries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Upkeep.Shell.Infrastructure.Base;
using Upkeep.Modules.DatabaseExplorer.Models;

namespace Upkeep.Modules.DatabaseExplorer.ViewModels
{
    public class QueryExplorerViewModel : BaseViewModel
    {
        public QueryExplorerViewModel()
        {
            this.ExecuteQueryCommand = new DelegateCommand<object>(this.OnExecuteQuery);
            this.DataGridDoubleClick = new DelegateCommand<object>(this.OnDataGridDoubleClick);
            PopulateSqlSnippets();
            OnExecuteQuery(null);
        }

        private string _queryString;
        public string QueryString
        {
            get { return _queryString; }
            set { SetProperty(ref this._queryString, value); }
        }

        private ObservableCollection<DataGridColumn> _columnCollection = new ObservableCollection<DataGridColumn>();
        public ObservableCollection<DataGridColumn> ColumnCollection
        {
            get
            {
                return this._columnCollection;
            }
            set
            {
                _columnCollection = value;
                base.OnPropertyChanged("ColumnCollection");
                //Error
                //base.OnPropertyChanged<ObservableCollection<DataGridColumn>>(() => this.ColumnCollection);
            }
        }

        private DataTable _datatable = new DataTable();

        public DataTable QueryDataTable
        {
            get
            {
                return _datatable;
            }
            set
            {
                if (_datatable != value)
                {
                    _datatable = value;
                }
                base.OnPropertyChanged("QueryDataTable");
            }
        }

        public ObservableCollection<SqlSnippet> SqlSnippets { get; set; }

        public ICommand ExecuteQueryCommand { get; private set; }
        private void OnExecuteQuery(object arg)
        {
            if (String.IsNullOrEmpty(QueryString))
                QueryString = "select top 10 * from doctors";

            //Initialize the view source and set the source to your observable collection
            List<dynamic> result = QueryHelper.ExecuteDynamicQuery(QueryString);
            PopulateDataTable(result);
        }

        public ICommand DataGridDoubleClick { get; private set; }
        private void OnDataGridDoubleClick(object arg)
        {
            if (SelectedSnippet != null)
                QueryString = SelectedSnippet.Sql;

            List<dynamic> result = QueryHelper.ExecuteDynamicQuery(QueryString);
            PopulateDataTable(result);
        }

        private SqlSnippet _selectedSnippet;
        public SqlSnippet SelectedSnippet
        {
            get { return _selectedSnippet; }
            set { SetProperty(ref this._selectedSnippet, value); }
        }

        public void PopulateDataTable(List<dynamic> items)
        {
            ColumnCollection = new ObservableCollection<DataGridColumn>();

            var data = items.ToArray();
            if (data.Count() == 0) return;

            QueryDataTable = new DataTable();
            foreach (var key in ((IDictionary<string, object>)data[0]).Keys)
            {
                QueryDataTable.Columns.Add(key);

                DataGridTextColumn tC = new DataGridTextColumn();
                tC.Header = key;
                tC.Binding = new System.Windows.Data.Binding(key);
                ColumnCollection.Add(tC);
            }
            foreach (var d in data)
            {
                QueryDataTable.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
            }
        }

        public void PopulateSqlSnippets()
        {
            SqlLibrary lib = new SqlLibrary();
            SqlSnippets = new ObservableCollection<SqlSnippet>(lib.SqlSnippets);
        }




        //public static ObservableCollection<GenericObject> Convert(DataTable toConvert)
        //{
        //    ObservableCollection<GenericObject> _result = new ObservableCollection<GenericObject>();

        //    foreach (DataRow _row in toConvert.Rows)
        //    {
        //        GenericObject _genericObject = new GenericObject();
        //        foreach (DataColumn _column in toConvert.Columns)
        //        {
        //            _genericObject.Properties.Add(new GenericProperty(_column.ColumnName, _row[_column]));
        //        }
        //        _result.Add(_genericObject);
        //    }

        //    return _result;
        //}

        //public static ObservableCollection<dynamic> Convert(List<dynamic> toConvert)
        //{
        //    ObservableCollection<dynamic> _result = new ObservableCollection<dynamic>();
        //    foreach (var item in toConvert)
        //    {
        //        _result.Add(item);
        //    }
        //    return _result;
        //}

        //public static ObservableCollection<DataTable> ConvertToObservableCollection(DataTable toConvert)
        //{
        //    ObservableCollection<DataTable> _result = new ObservableCollection<DataTable>();
        //    _result.Add(toConvert);
        //    return _result;
        //}
    }
}
