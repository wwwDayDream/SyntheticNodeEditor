using NodeEditor.JsonModels.Node;
using NodeEditor.JsonModels.Piece;

namespace NodeEditor.JsonModels;

public class CreationData {
    public int refIDCounter;
    public bool isFavorite;
    public string creationType;
    public string creationName;
    public string description;

    public NodeGraph NodeGraph;
	public PiecePart[] pieces;
	public Creation[] nestedCreationsRecipes;
	public NestedCreation[] nestedCreations;
}