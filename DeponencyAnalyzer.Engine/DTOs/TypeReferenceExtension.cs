﻿using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDeponencyAnalyzer.Engine.DTOs
{
   public  class TypeReferenceExtension
    {
        public TypeReferenceExtension ()
        {
            IsList = false;
            Name = string.Empty;
        }
       public TypeReference typeReference { set; get; }

        public bool IsList { set; get; }

        public string Name { set; get; }
    }
}
