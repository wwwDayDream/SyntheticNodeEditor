using System.IO;
using Newtonsoft.Json;
using NodeEditor.JsonModels;
using NodeEditor.JsonModels.Node;
using NodeEditor.JsonModels.Piece;

namespace NodeEditor.Models;

public partial class NodeViewModel {
    public static NodeConstructor[]? NodeConstructors {
        get
        {
            if (_nodeConstructors != null) return _nodeConstructors;
            
            var stream = typeof(MainWindow).Assembly
                .GetManifestResourceStream(typeof(MainWindow).Namespace + SyntheticNodeResourceName);
            var textReader = new StreamReader(stream);
            _nodeConstructors = JsonConvert.DeserializeObject<NodeConstructor[]>(textReader.ReadToEnd());
            textReader.Close();
            return _nodeConstructors;
        }
    }
    public static PieceConstructor[]? PieceConstructors {
        get
        {
            if (_pieceConstructors != null) return _pieceConstructors;
            
            var stream = typeof(MainWindow).Assembly
                .GetManifestResourceStream(typeof(MainWindow).Namespace + SyntheticPieceResourceName);
            var textReader = new StreamReader(stream);
            _pieceConstructors = JsonConvert.DeserializeObject<PieceConstructor[]>(textReader.ReadToEnd());
            textReader.Close();
            return _pieceConstructors;
        }
    }
    private const string SyntheticNodeResourceName = ".SyntheticNodes.dat";
    private const string SyntheticPieceResourceName = ".SyntheticPieces.dat";
    private static NodeConstructor[]? _nodeConstructors = null;
    private static PieceConstructor[]? _pieceConstructors = null;
}