using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.DTOs
{
    public class GraphNode
    {
   

        public GraphNode()
        {
            Childrens = new List<GraphNode>();
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public NodeType nodeType { set; get; }

        public List<DependencyType> dependencyType { set; get; }
        public string Name{ set; get; }

        public string Namespace { set; get; }

        public List<GraphNode> Childrens { set; get; }

     
    
         
    }
}
