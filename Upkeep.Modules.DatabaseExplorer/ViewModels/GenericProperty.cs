using System.ComponentModel;

namespace Upkeep.Modules.DatabaseExplorer.ViewModels
{
    public class GenericProperty : INotifyPropertyChanged
    {
        public GenericProperty(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}