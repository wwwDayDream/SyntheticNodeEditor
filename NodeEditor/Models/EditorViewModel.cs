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

	public enum NodeConstructorType {
		NODE,
		PIECE,
		NESTED,
		EXECUTION, //? Enter/Exit
		UNKNOWN
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

		GenerateNodes(creationObject.CreationData);

		foreach (NodeConnection connection in creationObject.CreationData.NodeGraph.nodeConnections) {
			NodeViewModel? from = Nodes.FirstOrDefault(n => n.ID == connection.fromNodeRefID);
			NodeViewModel? to = Nodes.FirstOrDefault(n => n.ID == connection.toNodeRefID);
			if (from == null || to == null) continue;

			try { //TODO Add the enter/exit ports so that the -1 ports dont cause this exception
				Connections.Add(new ConnectionViewModel (
					from.Output[connection.fromNodePortIndex + ((from.ConstructorType == NodeConstructorType.NODE || connection.isExecutionPort) ? 0 : 1)],
					to.Input[connection.toNodePortIndex + ((to.ConstructorType == NodeConstructorType.NODE || connection.isExecutionPort) ? 0 : 1)]
				));
				Console.WriteLine($"from: {connection.fromNodePortIndex}\nto: {connection.toNodePortIndex}");
			} catch (System.Exception e) {
				Console.WriteLine($"{from.Title} - Connection.Add: {e.Message}");
			}
		}
    }

	public void GenerateNodes(CreationData data) {
		//TODO Create a class to do this in there ti clean up this script a bit
		// data.NodeGraph.ExecutionEnterPosition.x
        foreach (var nodeGraphNode in data.NodeGraph.nodes)
        {
			int nodeID = nodeGraphNode.ID.First();
			int pieceID = -1; //? will be set in the switch if nodeID = -1
			Console.WriteLine($"\nID: {nodeGraphNode.refID}");
			Console.WriteLine($"linked piece: {nodeGraphNode.linkedPieceRefID}");
			
			NodeConstructorType constructorType = 
				(nodeID > -1) ? NodeConstructorType.NODE : 
				(nodeID == -1) ? NodeConstructorType.PIECE : 
				(nodeID == -2) ? NodeConstructorType.NESTED : 
				NodeConstructorType.UNKNOWN;

			string title = "undefined";
			PortConstructor[] inputPorts = [];
			PortConstructor[] outputPorts = [];

			switch (constructorType) {
				case NodeConstructorType.NODE: {
					NodeConstructor constructor = NodeViewModel.NodeConstructors[nodeID];
					title = constructor.nodeName;
					inputPorts = constructor.InputPorts;
					outputPorts = constructor.OutputPorts;
				} break;
				case NodeConstructorType.PIECE: {
					pieceID = Array.Find(data.pieces, p => p.refID == nodeGraphNode.linkedPieceRefID).pieceID;
					Console.WriteLine($"Piece ID: {pieceID}");
					PieceConstructor constructor = NodeViewModel.PieceConstructors[pieceID];
					title = constructor.pieceName;
					inputPorts = constructor.InputPorts;
					outputPorts = constructor.OutputPorts;
				} break;
				case NodeConstructorType.NESTED: {
					NestedCreation? nestedCreation = Array.Find(data.nestedCreations, part => part.refID == nodeGraphNode.linkedPieceRefID);
					Creation? nestedRecipe = Array.Find(data.nestedCreationsRecipes, creation => creation.CreationName == nestedCreation.partName);
					title = nestedCreation.partName;
					inputPorts = nestedRecipe.CreationData.NodeGraph.InputPorts;
					outputPorts = nestedRecipe.CreationData.NodeGraph.OutputPorts;
				}; break;
				default: continue;
			}

			Console.WriteLine($"Title: {title}");
            
			//TODO Improve node default positioning to start in the center
            Nodes.Add(new NodeViewModel() {
                Title = title,
				ID = nodeGraphNode.refID,
				ConstructorType = constructorType,
				//? x&y + window width * 0.4 and 0.2 : to center it more in the window from startup
				//! Dont forget to remove this once there are better positioning methods
                Location = new Point(
					nodeGraphNode.GraphPosition.X + (MainWindow.WinWidth * 0.4f), 
					-nodeGraphNode.GraphPosition.Y + (MainWindow.WinWidth * 0.2f)
				),
				Input = PortsToConnectors(inputPorts, constructorType),
				Output = PortsToConnectors(outputPorts, constructorType)
            });
        }
	}

	public ObservableCollection<ConnectorViewModel> PortsToConnectors(PortConstructor[] ports, NodeConstructorType type) {
		ObservableCollection<ConnectorViewModel> connectors = [];
		if (type == NodeConstructorType.PIECE || type == NodeConstructorType.NESTED) {
			connectors.Add(
				new ConnectorViewModel {
					Title = ">"
				}
			);
		}
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