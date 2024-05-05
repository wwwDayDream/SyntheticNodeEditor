using System.Collections.ObjectModel;

namespace NodeEditor.Models;

public class EditorViewModel
{
    public ObservableCollection<NodeViewModel> Nodes { get; } = new ObservableCollection<NodeViewModel>();
    public ObservableCollection<ConnectionViewModel> Connections { get; } = new ObservableCollection<ConnectionViewModel>();

    public EditorViewModel()
    {
        Nodes.Add(new NodeViewModel { Title = "Welcome", Input = {new ConnectorViewModel() { Title = "Test" }}});
        Nodes.Add(new NodeViewModel { Title = "Welcome", Output = {new ConnectorViewModel() { Title = "Test" }}});

    }
}