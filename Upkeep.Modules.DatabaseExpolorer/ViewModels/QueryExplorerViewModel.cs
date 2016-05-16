using Prism.Commands;
using RxDM.Entities.Queries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Upkeep.Shell.Infrastructure.Base;
namespace Upkeep.Modules.DatabaseExpolorer.ViewModels
{
    public class QueryExplorerViewModel : BaseViewModel
    {
        private string _queryString;
        public string QueryString
        {
            get { return _queryString; }
            set { SetProperty(ref this._queryString, value); }
        }


        private ObservableCollection<GenericObject> _queryResult;
        public ObservableCollection<GenericObject> QueryResult
        {
            get { return _queryResult; }
            set { SetProperty(ref this._queryResult, value); }
        }

        private ObservableCollection<dynamic> _dynamicQueryResult;
        public ObservableCollection<dynamic> DynamicQueryResult
        {
            get { return _dynamicQueryResult; }
            set { SetProperty(ref this._dynamicQueryResult, value); }
        }

        private ObservableCollection<DataTable> _tableQueryResult;
        public ObservableCollection<DataTable> TableQueryResult
        {
            get { return _tableQueryResult; }
            set { SetProperty(ref this._tableQueryResult, value); }
        }

        private ObservableCollection<IList> _listQueryResult;
        public ObservableCollection<IList> ListQueryResult
        {
            get { return _listQueryResult; }
            set { SetProperty(ref this._listQueryResult, value); }
        }

        public DataTable QueryDataTable { get; set; }
             


        public QueryExplorerViewModel()
        {
            this.ExecuteQueryCommand = new DelegateCommand<object>(this.OnExecuteQuery);
            OnExecuteQuery(null);
        }

        public ICommand ExecuteQueryCommand { get; private set; }
        private void OnExecuteQuery(object arg)
        {
            if (String.IsNullOrEmpty(QueryString))
                QueryString = "select top 10 * from doctors";

            //Initialize the view source and set the source to your observable collection
            QueryDataTable = QueryHelper.ExecuteQuery(QueryString);

            //var dt = QueryHelper.ExecuteQuery(QueryString);

            //if (QueryResult != null)
            //    QueryResult.Clear();

            

            //QueryResult = Convert(dt);
            //TableQueryResult = ConvertToObservableCollection(dt);

            //var dq = QueryHelper.ExecuteDynamicQuery(QueryString);
            //DynamicQueryResult = Convert(dq);
        }

        public static ObservableCollection<GenericObject> Convert(DataTable toConvert)
        {
            ObservableCollection<GenericObject> _result = new ObservableCollection<GenericObject>();

            foreach (DataRow _row in toConvert.Rows)
            {
                GenericObject _genericObject = new GenericObject();
                foreach (DataColumn _column in toConvert.Columns)
                {
                    _genericObject.Properties.Add(new GenericProperty(_column.ColumnName, _row[_column]));
                }
                _result.Add(_genericObject);
            }

            return _result;
        }

        public static ObservableCollection<dynamic> Convert(List<dynamic> toConvert)
        {
            ObservableCollection<dynamic> _result = new ObservableCollection<dynamic>();
            foreach (var item in toConvert)
            {
                _result.Add(item);
            }
            return _result;
        }

        public static ObservableCollection<DataTable> ConvertToObservableCollection(DataTable toConvert)
        {
            ObservableCollection<DataTable> _result = new ObservableCollection<DataTable>();
            _result.Add(toConvert);
            return _result;
        }

    }
}
