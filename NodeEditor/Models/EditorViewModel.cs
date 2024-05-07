using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using NodeEditor.JsonModels;
using NodeEditor.JsonModels.Misc;
using NodeEditor.JsonModels.Node;
using NodeEditor.JsonModels.Piece;

namespace NodeEditor.Models;

public class EditorViewModel
{
    public ICommand DisconnectConnectorCommand { get; }
    public ObservableCollection<NodeViewModel> Nodes { get; } = [ ];
    public ObservableCollection<ConnectionViewModel> Connections { get; } = [ ];
    public PendingConnectionViewModel PendingConnection { get; }

	private enum NodeConstructorType {
		node,
		piece,
		nested
	}

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
			int pieceID = Array.Find(creationObject.CreationData.pieces, p => p.refID == nodeGraphNode.linkedPieceRefID).pieceID;
			
			NodeConstructorType constructorType = 
				(nodeID > -1) ? NodeConstructorType.node : 
				(nodeID == -1) ? NodeConstructorType.piece : 
				NodeConstructorType.nested;

			string title = "undefined";
			PortConstructor[] inputPorts = [];
			PortConstructor[] outputPorts = [];

			switch (constructorType) {
				case NodeConstructorType.node: {
					NodeConstructor constructor = NodeViewModel.NodeConstructors[nodeID];
					title = constructor.nodeName;
					inputPorts = constructor.InputPorts;
					outputPorts = constructor.OutputPorts;
				} break;
				case NodeConstructorType.piece: {
					//!ERROR: Value cannot be null. (Parameter 'stream')
					//> ./NodeEditor\Models\NodeViewModeNodes.cs:line 30
					PieceConstructor constructor = NodeViewModel.PieceConstructors[pieceID];
					title = constructor.pieceName;
					inputPorts = constructor.InputPorts;
					outputPorts = constructor.OutputPorts;
				} break;
				case NodeConstructorType.nested: continue; //TODO
				default: continue;
			}

			Console.WriteLine(title);
            
			//TODO Improve node default positioning to start in the center
            Nodes.Add(new NodeViewModel() {
                Title = title,
				//? x&y + window width * 0.4 and 0.2 : to center it more in the window from startup
				//! Dont forget to remove this once there are better positioning methods
                Location = new Point(nodeGraphNode.GraphPosition.X + (MainWindow.WinWidth * 0.4f), -nodeGraphNode.GraphPosition.Y + (MainWindow.WinWidth * 0.2f)),
				Input = PortsToConnectors(inputPorts),
				Output = PortsToConnectors(outputPorts)
            });
        }
    }

	public ObservableCollection<ConnectorViewModel> PortsToConnectors(PortConstructor[] ports) {
		ObservableCollection<ConnectorViewModel> connectors = [];
		foreach (PortConstructor port in ports) {
			connectors.Add(
				new ConnectorViewModel {
					Title = port.portName
				}
			);
		}

		return connectors;
	}
}