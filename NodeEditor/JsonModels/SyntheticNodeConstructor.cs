namespace NodeEditor.JsonModels;

public class SyntheticNodeConstructor {
    public string className;
    public string description;
    public bool isExecutable;
    public string nodeName;
    public SyntheticPortConstructor[] ExecutionExits;
    public SyntheticPortConstructor[] InputPorts;
    public SyntheticPortConstructor[] OutputPorts;
}
