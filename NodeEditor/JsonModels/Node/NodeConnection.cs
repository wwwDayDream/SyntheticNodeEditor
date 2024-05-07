namespace NodeEditor.JsonModels.Node;

public class NodeConnection {
    public bool isExecutionPort;
    public int fromNodeRefID;
    public int fromNodePortIndex;
    public int toNodeRefID;
    public int toNodePortIndex;
}