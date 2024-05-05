namespace NodeEditor.SSJson;

public class SyntheticCreation {
    public string VersionTime;
    public string id;
    public string CreationName;
    public string CreatorId;
    public string CreationRootId;

    public SyntheticCreationData CreationData;
}

public class SyntheticCreationData {
    public int refIDCounter;
    public bool isFavorite;
    public string creationType;
    public string creationName;
    public string description;

    public SyntheticNodeGraph NodeGraph;
}

public class SyntheticNodeGraph {
    public string graphName;
    public bool isMethod;
    public int refID;
    public int refIDCounter;
    public V2Int ExecutionEnterPosition;
    public V2Int ExecutionExitPosition;
    public List<SyntheticPort> InputPorts;
    public List<SyntheticPort> OutputPorts;
    public List<SyntheticVariable> Variables;
}

public class SyntheticPort {
    public string portName;
    public string dataType;
}

public class SyntheticVariable {
    public string VariableName;
    public int RefID;
    public string Type;
    public string Value;
}

public class SyntheticValueNode {
    public int refID;
    public string ValueName;
}
public struct V2Int {
    public int x;
    public int y;
}