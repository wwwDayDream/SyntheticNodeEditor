using System.ComponentModel;
using System.Windows;

namespace NodeEditor.Models;

public class ConnectorViewModel : INotifyPropertyChanged
{
    private Point _anchor;
    public Point Anchor
    {
        set
        {
            _anchor = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Anchor)));
        }
        get => _anchor;
    }

    private bool _isConnected;
    public bool IsConnected
    {
        set
        {
            _isConnected = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsConnected)));
        }
        get => _isConnected;
    }

    public string Title { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}