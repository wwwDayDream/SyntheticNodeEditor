using System.Numerics;

namespace NodeEditor.JsonModels.Node;

public class Node {
	//* -1 = look up the piece with linkedPieceRefID
    public int[] ID;
    public int refID;
    public int linkedPieceRefID;
    public Vector2 GraphPosition;
}