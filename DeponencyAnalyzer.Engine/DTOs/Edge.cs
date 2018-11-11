using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.DTOs
{
    public  class Edge : BaseGraphType
    {
 

        public int id { set; get; }
        public bool dashes { set; get; }
        public string smooth { set; get; }
        public int from { set; get; }
        public int to { set; get; }
        public int length { set; get; }
        public int width { set; get; }
         public string arrows { set; get; }

        public Edge(int id  ,Node source , Node target ,  bool IsCircle)
        {
            this.id = id;
            this.from = source.id;
            this.to = target.id;
            this.width = 2;
            this.arrows = "to";
            this.title = $@"edge from {source.label} to {target.label}";
            if (IsCircle)
            {
                this.label = "circular\n dependency";
                this.color = "{color:#ff0000}";
                this.smooth = "{type:'curvedCCW',forceDirection:'none',roundness=1 }";
                this.dashes = false;
            }
            else
            {
                this.color = "{color:#848484}";
                this.label = string.Empty;
                this.smooth = "{type:'vertical',forceDirection:'none',roundness=0.5 }";
                this.dashes = true;
            }
        }
    }
}
