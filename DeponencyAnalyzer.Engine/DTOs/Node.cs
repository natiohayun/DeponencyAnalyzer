using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.DTOs
{
    public class Node : BaseGraphType
    {
        public int id { set; get; }
        public int value { set; get; }
        public string group { set; get; }
        public string shape { set; get; }

        public string htmlContent { set; get; }

        
        public NodeType nodeType { set; get; }

        public List<DependencyType> dependencyType { set; get; }
        public Node(int id, string label,List<DependencyType> dependencyType, NodeType nodeType,bool hasHiddenChildern)
        {
            this.label = label;

            this.id = id;
                this.size = 50;
            this.value = 5;
            this.group = "";
            this.dependencyType = dependencyType;
            this.nodeType = nodeType;

            if (nodeType == NodeType.Interface)
            {
                this.shape = "circle";
                this.color = "#ffeecc";
            }
            else if (nodeType == NodeType.AbstractClass)
            {
                this.shape = "square";
                this.color = "#d9e6f2";
            }
            else
            {
                this.shape = "box";
                this.color = "#9fbfdf";
            }

            if (hasHiddenChildern)
                this.color = "#ff8080";
            //var dependencyTypes = 
            //this.htmlContent = $@"<div class='card'><div class=subCat>Node Type:{nodeType}</div><div class=subCat>Dependency Type to parent node : {String.Join(",", dependencyType)}</div></div>";
        }
    }
}
