using DeponencyAnalyzer.Engine.DTOs;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeponencyAnalyzer.Engine.Interfaces
{
    public interface IActions<T,K>
    {
        List<T> BuildTypes(K moduleDefinition);


        void Run(string file);

        List<BaseGraphType> BuildRootGraph(List<GraphNode> nodes);

 
    }
}
