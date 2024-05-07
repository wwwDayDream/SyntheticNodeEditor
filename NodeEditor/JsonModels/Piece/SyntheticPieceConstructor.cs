namespace NodeEditor.JsonModels;

public class SyntheticPieceConstructor {
    public enum PieceAvailability { CreationOnly, LevelOnly, All }

    public PieceAvailability availability;
    public string description;
    public bool isFunctional;
    public string pieceName;
    public SyntheticPortConstructor[] ExecutionExits;
    public SyntheticPortConstructor[] InputPorts;
    public SyntheticPortConstructor[] OutputPorts;
    public SyntheticPieceConstructorProperty[] pieceConstructorProperties;
}