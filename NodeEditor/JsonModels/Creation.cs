using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NodeEditor.JsonModels;

public class Creation {
    public string VersionTime;
    public string id;
    public string CreationName;
    public string CreatorId;
    public string CreationRootId;

    public CreationData CreationData;

    public string ShareId;
    public string CreatedAt;
}