using Prism.Commands;
using RxDM.Entities.Queries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Upkeep.Modules.DatabaseExplorer.Models;
using Upkeep.Shell.Infrastructure.Base;

namespace Upkeep.Modules.DatabaseExplorer.ViewModels
{
    public class LocalDbManagerViewModel : BaseViewModel
    {
        public ObservableCollection<SqlConnectionInfo> DatabaseConnections { get; set; }

        public LocalDbManagerViewModel()
        {
            this.ListItemDoubleClickCommand = new DelegateCommand<object>(this.OnGetDatabaseInformation);
            DatabaseConnections = new ObservableCollection<SqlConnectionInfo>();
            PopulateConnectionStrings();
        }

        private void OnGetDatabaseInformation(object obj)
        {
            var sqlInfo = obj as ListViewItem;
            if (sqlInfo != null)
            {
                SelectedDatabaseConnection = sqlInfo.Content as SqlConnectionInfo;
            }
        }

        private SqlConnectionInfo _selectedDatabaseConnection;
        public SqlConnectionInfo SelectedDatabaseConnection
        {
            get { return _selectedDatabaseConnection; }
            set { SetProperty(ref this._selectedDatabaseConnection, value); }
        }

        public ICommand ListItemDoubleClickCommand
        {
            get; private set;
        }

        public void BackupDatabase(string filePath)
        {
            //using (TVend2014Entities dbEntities = new TVend2014Entities(BaseData.ConnectionString))
            //{
            //    string backupQuery = @"BACKUP DATABASE ""{0}"" TO DISK = N'{1}'";
            //    backupQuery = string.Format(backupQuery, "full databsase file path like C:\tempDb.mdf", filePath);
            //    dbEntities.Database.SqlQuery<object>(backupQuery).ToList().FirstOrDefault();
            //}
        }

        public void RestoreDatabase(string filePath)
        {
            //using (TVend2014Entities dbEntities = new TVend2014Entities(BaseData.ConnectionString))
            //{
            //    string restoreQuery = @"USE [Master]; ALTER DATABASE ""{0}"" SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE ""{0}"" FROM DISK='{1}' WITH REPLACE; ALTER DATABASE ""{0}"" SET MULTI_USER;";
            //    restoreQuery = string.Format(restoreQuery, "full db file path", filePath);
            //    var list = dbEntities.Database.SqlQuery<object>(restoreQuery).ToList();
            //    var resut = list.FirstOrDefault();
            //}
        }

        public void PopulateConnectionStrings()
        {
            foreach (ConnectionStringSettings connSettings in ConfigurationManager.ConnectionStrings)
            {
                try
                {
                    if (!String.IsNullOrEmpty(connSettings.ConnectionString.Trim()))
                    {
                        System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
                        builder.ConnectionString = connSettings.ConnectionString;
                        if (!String.IsNullOrEmpty(builder.InitialCatalog))
                        {
                            string dbLocation = QueryHelper.GetPhysicalDatabaseLocation(builder.InitialCatalog);
                            SqlConnectionInfo ci = new SqlConnectionInfo
                            {
                                Name = connSettings.Name,
                                ConnectionString = connSettings.ConnectionString,
                                ProviderName = connSettings.ProviderName,
                                DatabaseName = builder.InitialCatalog,
                                Server = builder.DataSource,
                                DatabaseLocation = dbLocation
                            };
                            DatabaseConnections.Add(ci);
                        }
                    }
                }
                catch(Exception ex)
                {
                    // TODO: Log this
                }
            }
        }
    }
}
