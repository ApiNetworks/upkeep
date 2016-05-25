using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upkeep.Modules.FileExplorer.Models;
using Upkeep.Shell.Infrastructure.Base;

namespace Upkeep.Modules.FileExplorer.ViewModels
{
    public class FolderExplorerViewModel : BaseViewModel
    {
        public FolderExplorerViewModel()
        {
            SelectedFiles = new ObservableCollection<FileInfoModel>();
        }

        public ObservableCollection<FileInfoModel> SelectedFiles { get; set; }


    }
}
