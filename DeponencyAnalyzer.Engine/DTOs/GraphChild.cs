using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.DTOs
{
   public class GraphChild
    {
        
        public NodeType nodeType { set; get; }
        public List<DependencyType> dependencyType { set; get; }
        public string Name { set; get; }

        
        
    }
}
