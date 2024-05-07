using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
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
            var creationObject = JsonConvert.DeserializeObject<Creation>(File.ReadAllText(dialogFileName));
            Load(creationObject);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        
    }

    private void Load(Creation? creationObject)
    {
        Nodes.Clear();
        Connections.Clear();
        
        foreach (var nodeGraphNode in creationObject.CreationData.NodeGraph.nodes)
        {	
			int nodeID = nodeGraphNode.ID.First();
            if (nodeID < 0) continue;
            
			//TODO Improve node default positioning to start in the center
            Nodes.Add(new NodeViewModel() {
                Title = NodeViewModel.NodeConstructors[nodeID].nodeName,
				//? x&y + window width * 0.4 and 0.2 : to center it more in the window from startup
				//! Dont forget to remove this once there are better positioning methods
                Location = new Point(nodeGraphNode.GraphPosition.X + (MainWindow.WinWidth * 0.4f), -nodeGraphNode.GraphPosition.Y + (MainWindow.WinWidth * 0.2f)),
				Input = new ObservableCollection<ConnectorViewModel> {
					new ConnectorViewModel {
						Title = "In"
					}
				},
				Output = new ObservableCollection<ConnectorViewModel> {
					new ConnectorViewModel {
						Title = "Out"
					}
				}
            });
            
        }
    }
}