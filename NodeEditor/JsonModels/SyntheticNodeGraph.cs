using System.Numerics;

namespace NodeEditor.JsonModels;

public class SyntheticNodeGraph {
    public string graphName;
    public bool isMethod;
    public int refID;
    public int refIDCounter;
    public Vector2 ExecutionEnterPosition;
    public Vector2 ExecutionExitPosition;
    public SyntheticPort[] InputPorts;
    public SyntheticPort[] OutputPorts;
    public SyntheticNode[] nodes;
    public SyntheticNodeConnection[] nodeConnections;
    public SyntheticVariable[] Variables;
    public SyntheticValueNode[] Values; // These are nodes and should be in .nodes but ye <3 Mak
}