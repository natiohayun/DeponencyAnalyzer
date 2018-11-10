using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.DTOs
{
   public class ParameterDefinitionExtension
    {

        public ParameterDefinitionExtension ()
        {
            IsList = false;
            Name = string.Empty;
        }
        public ParameterDefinition parameterDefinition { set; get; }

        public bool IsList { set; get; }

        public string Name { set; get; }

    }
}
