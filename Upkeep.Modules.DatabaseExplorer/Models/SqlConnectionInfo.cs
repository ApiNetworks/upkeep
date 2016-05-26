using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upkeep.Modules.DatabaseExplorer.Models
{
    public class SqlConnectionInfo 
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
        public string DatabaseLocation { get; set; }

        private string _defaultBackupLocation;

        public string DefaultBackupLocation
        {
            get
            {
                if (String.IsNullOrEmpty(_defaultBackupLocation) && !String.IsNullOrEmpty(DatabaseLocation))
                    return DatabaseLocation.ToLower().Replace(".mdf", ".bak");
                else
                    return _defaultBackupLocation;
            }
            set
            {
                _defaultBackupLocation = value;
            }
        }
    }
}
