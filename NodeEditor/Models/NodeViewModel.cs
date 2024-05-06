using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace NodeEditor.Models;

public partial class NodeViewModel : INotifyPropertyChanged {
    private Point _location;
    public Point Location
    {
        set
        {
            _location = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Location)));
        }
        get => _location;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public string Title { get; set; }
    public ObservableCollection<ConnectorViewModel> Input { get; set; } = new ObservableCollection<ConnectorViewModel>();
    public ObservableCollection<ConnectorViewModel> Output { get; set; } = new ObservableCollection<ConnectorViewModel>();

    
}