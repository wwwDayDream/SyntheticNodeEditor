using System.Numerics;

namespace NodeEditor.JsonModels.Node;

public class Node {
    public int[] ID;
    public int refID;
    public int linkedPieceRefID;
    public Vector2 GraphPosition;
}