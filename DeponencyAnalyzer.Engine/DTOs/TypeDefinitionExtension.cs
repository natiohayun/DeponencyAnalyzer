using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.DTOs
{
    public class TypeDefinitionExtension
    {
        public TypeDefinition typeDefinition { set; get; }
        public List<MethodDefinitionExtension> GetMethods()
        {
            List<MethodDefinitionExtension> methodDefinitionExtensionList = new List<MethodDefinitionExtension>();
            foreach (var method in typeDefinition.Methods)
                methodDefinitionExtensionList.Add(new MethodDefinitionExtension() { methodDefinition = method, Name = method.Name });
            return methodDefinitionExtensionList;
        }

        public List<FieldDefinitionExtension> GetFieldsDefinition(string projectNamespace)
        {
            List<FieldDefinitionExtension> list = new List<FieldDefinitionExtension>();
        

            foreach (var field in typeDefinition.Fields)
            {
                var type = GetType(field);


                if (field.FieldType.Namespace == projectNamespace)
                    list.Add(new FieldDefinitionExtension() { fieldDefinition = field,Name = field.FieldType.GetElementType().Name, Type = type, IsList = false});
                else if (field.FieldType.Namespace.ToString() == "System.Collections.Generic")
                {
                    if (field.FieldType.FullName.Contains(projectNamespace))
                    {
                        var array = field.FieldType.FullName.Split('<', '>');
                        foreach (var item in array)
                        {
                            if (item.Contains(projectNamespace))
                            {
                                var inside = item.Split('.');
                                if (inside.Count() == 2)
                                    list.Add(new FieldDefinitionExtension() { fieldDefinition = field,Name = inside[1], Type = type, IsList = true });
                           }
                        }
                    }
                }

            }
            return list;
        }

        private NodeType GetType(FieldDefinition field)
        {
            if (field.DeclaringType.IsInterface)
                return NodeType.Interface;
            else if (field.DeclaringType.IsAbstract)
                return NodeType.AbstractClass;
            else return NodeType.Class;
        }


    }
}
