using System.IO;
using Newtonsoft.Json;
using NodeEditor.JsonModels;

namespace NodeEditor.Models;

public partial class NodeViewModel {
    public static SyntheticNodeConstructor[]? NodeConstructors {
        get
        {
            if (_nodeConstructors != null) return _nodeConstructors;
            
            var stream = typeof(MainWindow).Assembly
                .GetManifestResourceStream(typeof(MainWindow).Namespace + SyntheticNodeResourceName);
            var textReader = new StreamReader(stream);
            _nodeConstructors = JsonConvert.DeserializeObject<SyntheticNodeConstructor[]>(textReader.ReadToEnd());
            textReader.Close();
            return _nodeConstructors;
        }
    }
    public static SyntheticPieceConstructor[]? PieceConstructors {
        get
        {
            if (_pieceConstructors != null) return _pieceConstructors;
            
            var stream = typeof(MainWindow).Assembly
                .GetManifestResourceStream(typeof(MainWindow).Namespace + SyntheticPieceResourceName);
            var textReader = new StreamReader(stream);
            _pieceConstructors = JsonConvert.DeserializeObject<SyntheticPieceConstructor[]>(textReader.ReadToEnd());
            textReader.Close();
            return _pieceConstructors;
        }
    }
    private const string SyntheticNodeResourceName = ".SyntheticNodes.dat";
    private const string SyntheticPieceResourceName = ".SyntheticPieces.dat";
    private static SyntheticNodeConstructor[]? _nodeConstructors = null;
    private static SyntheticPieceConstructor[]? _pieceConstructors = null;
}