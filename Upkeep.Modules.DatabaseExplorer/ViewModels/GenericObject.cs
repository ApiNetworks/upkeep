using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upkeep.Modules.DatabaseExplorer.ViewModels
{
    public class GenericObject
    {
        private readonly ObservableCollection<GenericProperty> properties = new ObservableCollection<GenericProperty>();

        public GenericObject(params GenericProperty[] properties)
        {
            foreach (var property in properties)
                Properties.Add(property);
        }

        public ObservableCollection<GenericProperty> Properties
        {
            get { return properties; }
        }

        private static ObservableCollection<GenericObject> Convert(DataTable toConvert)
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
    }
}
