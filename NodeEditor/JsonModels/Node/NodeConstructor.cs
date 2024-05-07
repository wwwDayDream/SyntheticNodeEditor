using NodeEditor.JsonModels.Misc;

namespace NodeEditor.JsonModels.Node;

public class NodeConstructor {
    public string className;
    public string description;
    public bool isExecutable;
    public string nodeName;
    public PortConstructor[] ExecutionExits;
    public PortConstructor[] InputPorts;
    public PortConstructor[] OutputPorts;
}
