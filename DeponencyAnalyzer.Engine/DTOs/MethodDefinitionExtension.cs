using CDeponencyAnalyzer.Engine.DTOs;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.DTOs
{
   public class MethodDefinitionExtension
    {
        public MethodDefinition methodDefinition { set; get; }
        public string Name { set; get; }

        public List<ParameterDefinitionExtension> GetFuncationParameters(string projectNamespace)
        {
            List<ParameterDefinitionExtension> parameterDefinitionList = new List<ParameterDefinitionExtension>();
            foreach(var parameter in methodDefinition.Parameters)
            {
                if(parameter.ParameterType.Namespace == projectNamespace)
                    parameterDefinitionList.Add(new ParameterDefinitionExtension() { parameterDefinition = parameter, IsList = false ,Name = parameter.ParameterType.Name});
                else if (parameter.ParameterType.Namespace.ToString()== "System.Collections.Generic")
                {
                    if(parameter.ParameterType.FullName.Contains(projectNamespace))
                    {
                        var array = parameter.ParameterType.FullName.Split('<','>');
                        foreach(var item in array)
                        {
                            if(item.Contains(projectNamespace))
                            {
                                var inside = item.Split('.');
                                if(inside.Count()==2)
                                    parameterDefinitionList.Add(new ParameterDefinitionExtension() { parameterDefinition = parameter, IsList = true, Name= inside[1] });
                            }
                        }
                    }
                }

            }

            return parameterDefinitionList;
        }

        public TypeReferenceExtension GetReturnType(string projectNamespace)
        {
            if (methodDefinition.ReturnType.Namespace == projectNamespace)
                return new TypeReferenceExtension() { typeReference = methodDefinition.ReturnType, IsList = false,Name = methodDefinition.Name };
            else if (methodDefinition.ReturnType.Namespace.ToString()== "System.Collections.Generic")
            {
                if (methodDefinition.ReturnType.FullName.Contains(projectNamespace))
                {
                    var array = methodDefinition.ReturnType.FullName.Split('<', '>');
                    foreach (var item in array)
                    {
                        if (item.Contains(projectNamespace))
                        {
                            var inside = item.Split('.');
                            if (inside.Count() == 2)
                                return new TypeReferenceExtension() { typeReference = methodDefinition.ReturnType, IsList = true, Name = inside[1] } ;
                        }
                    }
                }
            }
               
            return null;
        }
        public List<ParameterDefinitionExtension> GetFuncationAttributes(string projectNamespace)
        {
            List<ParameterDefinitionExtension> parameterDefinitionList = new List<ParameterDefinitionExtension>();

            if (methodDefinition.HasBody && methodDefinition.Body.HasVariables)
            {
                foreach (var parameter in methodDefinition.Parameters)
                {
                    if (parameter.ParameterType.Namespace == projectNamespace)
                        parameterDefinitionList.Add(new ParameterDefinitionExtension() { parameterDefinition = parameter, IsList = false, Name = parameter.Name });
                    else if (parameter.ParameterType.Namespace.ToString() == "System.Collections.Generic")
                    {
                        if (parameter.ParameterType.FullName.Contains(projectNamespace))
                        {
                            var array = parameter.ParameterType.FullName.Split('<', '>');
                            foreach (var item in array)
                            {
                                if (item.Contains(projectNamespace))
                                {
                                    var inside = item.Split('.');
                                    if (inside.Count() == 2)
                                        parameterDefinitionList.Add(new ParameterDefinitionExtension() { parameterDefinition = parameter, IsList = true, Name = inside[1] });
                                }
                            }
                        }
                    }
                }
            }
            return parameterDefinitionList;
        }
    }
}
