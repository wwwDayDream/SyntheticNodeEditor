using System.Numerics;
using NodeEditor.JsonModels.Misc;

namespace NodeEditor.JsonModels.Node;

public class NodeGraph {
    public string graphName;
    public bool isMethod;
    public int refID;
    public int refIDCounter;
    public Vector2 ExecutionEnterPosition;
    public Vector2 ExecutionExitPosition;
    public PortConstructor[] InputPorts;
    public PortConstructor[] OutputPorts;
    public Node[] nodes;
    public NodeConnection[] nodeConnections;
    public Variable[] Variables;
    public ValueNode[] Values; // These are nodes and should be in .nodes but ye <3 Mak
}