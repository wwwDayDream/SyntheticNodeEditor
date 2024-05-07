using NodeEditor.JsonModels.Node;

namespace NodeEditor.JsonModels;

public class CreationData {
    public int refIDCounter;
    public bool isFavorite;
    public string creationType;
    public string creationName;
    public string description;

    public NodeGraph NodeGraph;
}