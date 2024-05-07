using NodeEditor.JsonModels.Misc;

namespace NodeEditor.JsonModels.Piece;

public class PieceConstructor {
    public enum PieceAvailability { CreationOnly, LevelOnly, All }

    public PieceAvailability availability;
    public string description;
    public bool isFunctional;
    public string pieceName;
    public PortConstructor[] ExecutionExits;
    public PortConstructor[] InputPorts;
    public PortConstructor[] OutputPorts;
    public PieceConstructorProperty[] pieceConstructorProperties;
}