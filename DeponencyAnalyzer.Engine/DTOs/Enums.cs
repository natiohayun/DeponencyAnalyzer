using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.DTOs
{



    public enum AnalyzerType { CBO = 1 };
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NodeType { Class = 0, AbstractClass = 1, Interface = 2 }
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DependencyType { FunctionParameter = 0, ClassAttribute = 1, FunctionAttribute = 2, FunctionReturnValue = 3 }
}
