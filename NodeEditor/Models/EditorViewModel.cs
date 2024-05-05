using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Newtonsoft.Json;
using NodeEditor.JsonModels;

namespace NodeEditor.Models;

public class EditorViewModel
{
    public ICommand DisconnectConnectorCommand { get; }
    public ObservableCollection<NodeViewModel> Nodes { get; } = [ ];
    public ObservableCollection<ConnectionViewModel> Connections { get; } = [ ];
    public PendingConnectionViewModel PendingConnection { get; }

    public EditorViewModel()
    {
        PendingConnection = new PendingConnectionViewModel(this);
        
        Nodes.Add(new NodeViewModel { Title = "Welcome", Input = {new ConnectorViewModel() { Title = "Test" }}});
        Nodes.Add(new NodeViewModel { Title = "Welcome", Output = {new ConnectorViewModel() { Title = "Test" }}});
        
        DisconnectConnectorCommand = new DelegateCommand<ConnectorViewModel>(connector =>
        {
            var connection = Connections.First(x => x.Source == connector || x.Target == connector);
            connection.Source.IsConnected = false;  // This is not correct if there are multiple connections to the same connector
            connection.Target.IsConnected = false;
            Connections.Remove(connection);
        });
    }
    
    public void Connect(ConnectorViewModel source, ConnectorViewModel target)
    {
        Connections.Add(new ConnectionViewModel(source, target));
    }

    public void OnFileOpened(string dialogFileName)
    {
        Console.WriteLine(dialogFileName);
        if (!File.Exists(dialogFileName)) return;

        try
        {
            var creationObject = JsonConvert.DeserializeObject<SyntheticCreation>(File.ReadAllText(dialogFileName));
            Console.WriteLine(creationObject);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        
    }
}