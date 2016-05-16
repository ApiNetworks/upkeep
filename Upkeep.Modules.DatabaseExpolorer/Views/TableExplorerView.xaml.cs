using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Upkeep.Modules.DatabaseExpolorer.ViewModels;

namespace Upkeep.Modules.DatabaseExpolorer.Views
{
    /// <summary>
    /// Interaction logic for TableExplorerView.xaml
    /// </summary>
    public partial class TableExplorerView : UserControl
    {
        public TableExplorerView()
        {
            InitializeComponent();

            TableExplorerViewModel model = new TableExplorerViewModel();
            this.DataContext = model;
        }
    }
}
