using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Upkeep.Modules.DatabaseExplorer.Behaviours
{
    public class DataGridColumnsBehavior
    {
        public static readonly DependencyProperty BindableColumnsProperty =
            DependencyProperty.RegisterAttached("BindableColumns",
                                                typeof(ObservableCollection<DataGridColumn>),
                                                typeof(DataGridColumnsBehavior),
                                                new UIPropertyMetadata(null, BindableColumnsPropertyChanged));
        private static void BindableColumnsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = source as DataGrid;
            ObservableCollection<DataGridColumn> columns = e.NewValue as ObservableCollection<DataGridColumn>;
            dataGrid.Columns.Clear();
            if (columns == null)
            {
                return;
            }
            foreach (DataGridColumn column in columns)
            {
                dataGrid.Columns.Add(column);
            }
            columns.CollectionChanged += (sender, e2) =>
            {
                NotifyCollectionChangedEventArgs ne = e2 as NotifyCollectionChangedEventArgs;
                if (ne.Action == NotifyCollectionChangedAction.Reset)
                {
                    dataGrid.Columns.Clear();
                    if (ne.NewItems != null)
                    {
                        foreach (DataGridColumn column in ne.NewItems)
                        {
                            dataGrid.Columns.Add(column);
                        }
                    }
                }
                else if (ne.Action == NotifyCollectionChangedAction.Add)
                {
                    if (ne.NewItems != null)
                    {
                        foreach (DataGridColumn column in ne.NewItems)
                        {
                            dataGrid.Columns.Add(column);
                        }
                    }
                }
                else if (ne.Action == NotifyCollectionChangedAction.Move)
                {
                    dataGrid.Columns.Move(ne.OldStartingIndex, ne.NewStartingIndex);
                }
                else if (ne.Action == NotifyCollectionChangedAction.Remove)
                {
                    if (ne.OldItems != null)
                    {
                        foreach (DataGridColumn column in ne.OldItems)
                        {
                            dataGrid.Columns.Remove(column);
                        }
                    }
                }
                else if (ne.Action == NotifyCollectionChangedAction.Replace)
                {
                    dataGrid.Columns[ne.NewStartingIndex] = ne.NewItems[0] as DataGridColumn;
                }
            };
        }
        public static void SetBindableColumns(DependencyObject element, ObservableCollection<DataGridColumn> value)
        {
            element.SetValue(BindableColumnsProperty, value);
        }
        public static ObservableCollection<DataGridColumn> GetBindableColumns(DependencyObject element)
        {
            return (ObservableCollection<DataGridColumn>)element.GetValue(BindableColumnsProperty);
        }
    }

    public class Behaviours
    {
        public static DependencyProperty DoubleClickCommandProperty =
           DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand), typeof(Behaviours), new PropertyMetadata(DoubleClick_PropertyChanged));

        public static void SetDoubleClickCommand(UIElement element, ICommand value)
        {
            element.SetValue(DoubleClickCommandProperty, value);
        }

        public static ICommand GetDoubleClickCommand(UIElement element)
        {
            return (ICommand)element.GetValue(DoubleClickCommandProperty);
        }

        private static void DoubleClick_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var row = d as DataGridRow;
            if (row == null) return;

            if (e.NewValue != null)
            {
                row.AddHandler(DataGridRow.MouseDoubleClickEvent, new RoutedEventHandler(DataGrid_MouseDoubleClick));
            }
            else
            {
                row.RemoveHandler(DataGridRow.MouseDoubleClickEvent, new RoutedEventHandler(DataGrid_MouseDoubleClick));
            }
        }

        private static void DataGrid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;

            if (row != null)
            {
                var cmd = GetDoubleClickCommand(row);
                if (cmd.CanExecute(row.Item))
                    cmd.Execute(row.Item);
            }
        }
    }

    public static class Commands
    {
        public static readonly DependencyProperty DataGridDoubleClickProperty = DependencyProperty.RegisterAttached("DataGridDoubleClickCommand", typeof(ICommand), typeof(Commands), new PropertyMetadata(new PropertyChangedCallback(AttachOrRemoveDataGridDoubleClickEvent)));

        public static ICommand GetDataGridDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DataGridDoubleClickProperty);
        }

        public static void SetDataGridDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DataGridDoubleClickProperty, value);
        }

        public static void AttachOrRemoveDataGridDoubleClickEvent(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            DataGrid dataGrid = obj as DataGrid;
            if (dataGrid != null)
            {
                ICommand cmd = (ICommand)args.NewValue;

                if (args.OldValue == null && args.NewValue != null)
                {
                    dataGrid.MouseDoubleClick += ExecuteDataGridDoubleClick;
                }
                else if (args.OldValue != null && args.NewValue == null)
                {
                    dataGrid.MouseDoubleClick -= ExecuteDataGridDoubleClick;
                }
            }
        }

        private static void ExecuteDataGridDoubleClick(object sender, MouseButtonEventArgs args)
        {
            DependencyObject obj = sender as DependencyObject;
            ICommand cmd = (ICommand)obj.GetValue(DataGridDoubleClickProperty);
            if (cmd != null)
            {
                if (cmd.CanExecute(obj))
                {
                    cmd.Execute(obj);
                }
            }
        }
    }
}
