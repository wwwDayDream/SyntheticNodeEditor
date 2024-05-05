using System.Numerics;

namespace NodeEditor.SSJsonModels;

public class SyntheticNodeGraph {
    public string graphName;
    public bool isMethod;
    public int refID;
    public int refIDCounter;
    public Vector2 ExecutionEnterPosition;
    public Vector2 ExecutionExitPosition;
    public List<SyntheticPort> InputPorts;
    public List<SyntheticPort> OutputPorts;
    public List<SyntheticNode> nodes;
    public List<SyntheticNodeConnection> nodeConnections;
    public List<SyntheticVariable> Variables;
    public List<SyntheticValueNode> Values; // These are nodes and should be in .nodes but ye <3 Mak
}