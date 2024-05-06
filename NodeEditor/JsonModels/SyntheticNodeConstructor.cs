namespace NodeEditor.JsonModels;

public class SyntheticNodeConstructor {
    public string className;
    public string description;
    public bool isExecutable;
    public string nodeName;
    public SyntheticPort[] ExecutionExits;
    public SyntheticPort[] InputPorts;
    public SyntheticPort[] OutputPorts;
}
