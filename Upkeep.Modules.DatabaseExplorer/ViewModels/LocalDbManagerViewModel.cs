using Prism.Commands;
using RxDM.Entities.Queries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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

            this.BackupDatabaseCommand = DelegateCommand.FromAsyncHandler(BackupDatabaseAsync);
            this.RestoreDatabaseCommand = DelegateCommand.FromAsyncHandler(RestoreDatabaseAsync);

            this.IsDatabaseAvailable = false;

            DatabaseConnections = new ObservableCollection<SqlConnectionInfo>();
            PopulateConnectionStrings();
        }

        #region Commands

        private bool _isDatabaseAvailable;
        public bool IsDatabaseAvailable
        {
            get { return _isDatabaseAvailable; }
            set
            {
                SetProperty(ref this._isDatabaseAvailable, value);
                BackupDatabaseCommand.RaiseCanExecuteChanged();
                RestoreDatabaseCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanInteractWithDatabase()
        {
            return IsDatabaseAvailable;
        }

        private async Task RestoreDatabaseAsync()
        {
            if (!String.IsNullOrEmpty(DatabaseFullBackupPath) && File.Exists(DatabaseFullBackupPath))
            {
                IsDatabaseAvailable = false;
                await RestoreDatabase(DatabaseFullBackupPath);
                IsDatabaseAvailable = true;
            }
        }

        private async Task BackupDatabaseAsync()
        {
            if (!String.IsNullOrEmpty(DatabaseFullBackupPath))
            {
                IsDatabaseAvailable = false;
                await BackupDatabase(DatabaseFullBackupPath);
                IsDatabaseAvailable = true;
            }
        }

        private void OnGetDatabaseInformation(object obj)
        {
            var sqlInfo = obj as ListViewItem;
            if (sqlInfo != null)
            {
                SelectedDatabaseConnection = sqlInfo.Content as SqlConnectionInfo;
                DatabaseFullBackupPath = SelectedDatabaseConnection.DefaultBackupLocation;
                IsDatabaseAvailable = true;
            }
        }

        public ICommand ListItemDoubleClickCommand
        {
            get; private set;
        }

        public DelegateCommand BackupDatabaseCommand
        {
            get; private set;
        }

        public DelegateCommand RestoreDatabaseCommand
        {
            get; private set;
        }

        #endregion

        private SqlConnectionInfo _selectedDatabaseConnection;
        public SqlConnectionInfo SelectedDatabaseConnection
        {
            get { return _selectedDatabaseConnection; }
            set { SetProperty(ref this._selectedDatabaseConnection, value); }
        }

        private string _databaseFullBackupPath;
        public string DatabaseFullBackupPath
        {
            get { return _databaseFullBackupPath; }
            set { SetProperty(ref this._databaseFullBackupPath, value); }
        }

        public async Task<bool> BackupDatabase(string filePath)
        {
            return await QueryHelper.TryBackupDatabaseAsync(filePath);
        }

        public async Task<bool> RestoreDatabase(string filePath)
        {
            return await QueryHelper.TryRestoreDatabaseAsync(filePath);
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
                catch (Exception ex)
                {
                    // TODO: Log this
                }
            }
        }
    }
}
