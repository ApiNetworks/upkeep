using System.Windows;
using System.Windows.Controls;
using Upkeep.Modules.DatabaseExplorer.ViewModels;

namespace Upkeep.Modules.DatabaseExplorer.Views
{
    /// <summary>
    /// Interaction logic for QueryExplorerView.xaml
    /// </summary>
    public partial class QueryExplorerView : UserControl
    {
        public QueryExplorerView()
        {
            InitializeComponent();

            QueryExplorerViewModel model = new QueryExplorerViewModel();
            this.DataContext = model;
        }
    }
}
