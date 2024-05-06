using System.Collections.ObjectModel;

namespace NodeEditor.Models;

public partial class NodeViewModel {
    public string Title { get; set; }
    public ObservableCollection<ConnectorViewModel> Input { get; set; } = new ObservableCollection<ConnectorViewModel>();
    public ObservableCollection<ConnectorViewModel> Output { get; set; } = new ObservableCollection<ConnectorViewModel>();

    
}