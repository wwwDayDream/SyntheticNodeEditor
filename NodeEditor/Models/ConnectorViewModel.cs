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

    public string Title { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}