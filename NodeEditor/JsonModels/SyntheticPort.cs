namespace NodeEditor.JsonModels;

public class SyntheticPort {
    public enum SerializableType { Bool, Int, Float, String, Vector2, Vector3, Quaternion }
    public SerializableType dataType;
    public string portName;
}