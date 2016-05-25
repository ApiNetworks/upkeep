using System.Windows.Controls;
using System.Windows.Input;
using Upkeep.Modules.DatabaseExplorer.ViewModels;

namespace Upkeep.Modules.DatabaseExplorer.Views
{
    /// <summary>
    /// Interaction logic for LocalDbManagerView.xaml
    /// </summary>
    public partial class LocalDbManagerView : UserControl
    {
        public LocalDbManagerView()
        {
            InitializeComponent();
            LocalDbManagerViewModel model = new LocalDbManagerViewModel();
            this.DataContext = model;
        }

    }
}
