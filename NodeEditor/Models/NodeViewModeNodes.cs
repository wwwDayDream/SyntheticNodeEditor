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
                .GetManifestResourceStream(typeof(MainWindow).Namespace + SyntheticResourceName);
            var textReader = new StreamReader(stream);
            _nodeConstructors = JsonConvert.DeserializeObject<SyntheticNodeConstructor[]>(textReader.ReadToEnd());
            textReader.Close();
            return _nodeConstructors;
        }
    }
    private const string SyntheticResourceName = ".SyntheticNodes.dat";
    private static SyntheticNodeConstructor[]? _nodeConstructors = null;
}