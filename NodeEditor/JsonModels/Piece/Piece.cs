namespace NodeEditor.JsonModels.Piece;

//? PiecePart instead of just Piece because otherwise it would conflict with the namespace
public class PiecePart {
    public int pieceID;
    public int refID;
    public string pieceName;
	//?? Do we also need the "pieceProperties" array?

    //TODO Create PieceColor.cs to also store the color to use for later detaild
	//> public PieceColor pieceColor;
}