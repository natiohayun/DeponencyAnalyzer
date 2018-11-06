using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.DTOs
{
    public  class Edge : BaseGraphType
    {
 
        public int from { set; get; }
        public int to { set; get; }
        public int length { set; get; }
        public int width { set; get; }
         public string arrows { set; get; }

        public Edge(Node source , Node target , string label )
        {
            this.label = label;
            this.from = source.id;
            this.to = target.id;
            this.length = 100;
            this.width = 2;
            this.arrows = "to";
        }
    }
}
