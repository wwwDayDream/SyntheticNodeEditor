using System.Collections.ObjectModel;
using System.Windows;

namespace NodeEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow()
    {
        InitializeComponent();
    }
}
public class ConnectorViewModel
{
    public string Title { get; set; }
}
public class NodeViewModel
{
    public string Title { get; set; }
    public ObservableCollection<ConnectorViewModel> Input { get; set; } = new ObservableCollection<ConnectorViewModel>();
    public ObservableCollection<ConnectorViewModel> Output { get; set; } = new ObservableCollection<ConnectorViewModel>();

}

public class EditorViewModel
{
    public ObservableCollection<NodeViewModel> Nodes { get; } = new ObservableCollection<NodeViewModel>();

    public EditorViewModel()
    {
        Nodes.Add(new NodeViewModel { Title = "Welcome", Input = {new ConnectorViewModel() { Title = "Test" }}});
        Nodes.Add(new NodeViewModel { Title = "Welcome", Output = {new ConnectorViewModel() { Title = "Test" }}});

    }
}