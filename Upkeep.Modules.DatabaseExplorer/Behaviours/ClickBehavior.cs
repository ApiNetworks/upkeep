using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Upkeep.Modules.DatabaseExplorer.Behaviours
{
    public class ClickBehavior
    {
        public static DependencyProperty DoubleClickCommandProperty = DependencyProperty.RegisterAttached("DoubleClick",
                   typeof(ICommand),
                   typeof(ClickBehavior),
                   new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DoubleClickChanged)));

        public static void SetDoubleClick(DependencyObject target, ICommand value)
        {
            target.SetValue(ClickBehavior.DoubleClickCommandProperty, value);
        }

        public static ICommand GetDoubleClick(DependencyObject target)
        {
            return (ICommand)target.GetValue(DoubleClickCommandProperty);
        }

        private static void DoubleClickChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ListViewItem element = target as ListViewItem;
            if (element != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    element.MouseDoubleClick += element_MouseDoubleClick;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    element.MouseDoubleClick -= element_MouseDoubleClick;
                }
            }
        }

        private static void element_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject obj = sender as DependencyObject;
            ICommand command = (ICommand)obj.GetValue(DoubleClickCommandProperty);
            if (command != null)
            {
                if (command.CanExecute(obj))
                {
                    command.Execute(obj);
                }
            }
        }
    }
}
