using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.DTOs
{
   public class FieldDefinitionExtension
    {

        public FieldDefinitionExtension()
        {
            IsList = false;
            ListTypeName = string.Empty;
        }
       public FieldDefinition fieldDefinition { set; get; }

        public NodeType Type { set; get; }
        public string Name { set; get; }

        public bool IsList { set; get; }

        public string ListTypeName { set; get; }
    }
}
