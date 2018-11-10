
using DeponencyAnalyzer.Engine.DTOs;
using DeponencyAnalyzer.Engine.Interfaces;
using DeponencyAnalyzer.Engine.Workers;
using Mono.Cecil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.Workers
{
    public class CBOAction : IActions<TypeDefinitionExtension, ModuleDefinition>
    {
        private IFileActions fileaction;
        private HtmlReport report;
        private IHistory<string> history;
        public CBOAction ()
        {
            fileaction = new FileActions();
            report = new HtmlReport();
            history = new FileHistory();
        }
        public List<TypeDefinitionExtension> BuildTypes(ModuleDefinition moduleDefinition)
        {
            try
            {
                List<TypeDefinitionExtension> root = new List<TypeDefinitionExtension>();

                foreach (TypeDefinition type in moduleDefinition.Types)
                {
                    if (type.IsClass && type.Name != "<Module>")
                    {
                        root.Add( new TypeDefinitionExtension() { typeDefinition = type });
                    }
                }
                return root;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void Run(string file)
        {
            try
            {
                var module = fileaction.LoadFile(file);
                if (module != null)
                {
                    List<TypeDefinitionExtension> source = BuildTypes(module);
                    List<GraphNode> graphs = new List<GraphNode>();
                    foreach (var node in source)
                    {

                        GraphNode cbo = new GraphNode();
                        if (node.typeDefinition.IsInterface)
                            cbo.nodeType = NodeType.Interface;
                        else if (node.typeDefinition.IsAbstract)
                            cbo.nodeType = NodeType.AbstractClass;
                        else cbo.nodeType = NodeType.Class;
                        cbo.Name = node.typeDefinition.Name;
                        cbo.Namespace = node.typeDefinition.Namespace;
                        cbo.dependencyType = new List<DependencyType>();
                        cbo.Childrens = new List<GraphNode>();
                        var fields = node.GetFieldsDefinition(cbo.Namespace);
                        foreach (var field in fields)
                        {
                            if (field.Name != cbo.Name)
                            {
                                var exist = cbo.Childrens.Where(x => x.Name == field.Name).FirstOrDefault();
                                if (exist == null)
                                {
                                    GraphNode childNode = new GraphNode();
                                    childNode.Name = field.Name;
                                    if (field.fieldDefinition.DeclaringType.MetadataType == MetadataType.Class)
                                        childNode.nodeType = NodeType.Class;
                                    childNode.dependencyType = new List<DependencyType>();
                                  
                                    childNode.dependencyType.Add(DependencyType.ClassAttribute);
                                    cbo.Childrens.Add(childNode);
                                }
                                else if (!exist.dependencyType.Contains(DependencyType.ClassAttribute))
                                    exist.dependencyType.Add(DependencyType.ClassAttribute);
                            }
                        }

                        var methods = node.GetMethods();
                        foreach (var method in methods)
                        {

                            var returnType = method.GetReturnType(cbo.Namespace);
                            if (returnType != null)
                            {
                                if (returnType.Name != cbo.Name)
                                {
                                    var exist = cbo.Childrens.Where(x => x.Name == returnType.Name).FirstOrDefault();
                                    if (exist == null)
                                    {
                                        GraphNode childNode = new GraphNode();
                                        if (returnType.IsList)
                                            childNode.Name = returnType.Name;
                                        else childNode.Name = returnType.typeReference.Name;

                                        childNode.nodeType = NodeType.Class;
                                        childNode.dependencyType = new List<DependencyType>();
                                        childNode.dependencyType.Add(DependencyType.FunctionReturnValue);
                                        cbo.Childrens.Add(childNode);
                                    }
                                    else if (!exist.dependencyType.Contains(DependencyType.FunctionReturnValue))
                                        exist.dependencyType.Add(DependencyType.FunctionReturnValue);
                                }
                            }
                            var funcationParameters = method.GetFuncationParameters(cbo.Namespace);
                            foreach (var parameter in funcationParameters)
                            {
                                if (parameter.Name != cbo.Name)
                                {
                                    var exist = cbo.Childrens.Where(x => x.Name == parameter.Name).FirstOrDefault();
                                    if (exist == null)
                                    {
                                        GraphNode childNode = new GraphNode();
                                        if (parameter.IsList)
                                            childNode.Name = parameter.Name;
                                        else childNode.Name = parameter.Name;
                                        childNode.nodeType = NodeType.Class;
                                        childNode.dependencyType = new List<DependencyType>();
                                        childNode.dependencyType.Add(DependencyType.FunctionParameter);
                                        cbo.Childrens.Add(childNode);

                                    }
                                    else if (!exist.dependencyType.Contains(DependencyType.FunctionParameter))
                                        exist.dependencyType.Add(DependencyType.FunctionParameter);
                                }
                            }
                            var funcationAttribute = method.GetFuncationAttributes(cbo.Namespace);
                            foreach (var attribute in funcationAttribute)
                            {
                                if (attribute.Name != cbo.Name)
                                {
                                    var exist = cbo.Childrens.Where(x => x.Name == attribute.Name).FirstOrDefault();
                                    if (exist == null)
                                    {
                                        GraphNode childNode = new GraphNode();
                                        if (attribute.IsList)
                                            childNode.Name = attribute.Name;
                                        else childNode.Name = attribute.parameterDefinition.ParameterType.Name;
                                        childNode.Name = attribute.parameterDefinition.ParameterType.Name;
                                        childNode.nodeType = NodeType.Class;
                                        childNode.dependencyType = new List<DependencyType>();
                                        childNode.dependencyType.Add(DependencyType.FunctionAttribute);
                                        cbo.Childrens.Add(childNode);
                                    }
                                    else if (!exist.dependencyType.Contains(DependencyType.FunctionAttribute))
                                        exist.dependencyType.Add(DependencyType.FunctionAttribute);
                                }
                            }
                        }
                        graphs.Add(cbo);
                    }

                    CalculateDependencyIssues(graphs);
                    var data = BuildRootGraph(graphs);
            
                    
                  string contant =   report.BuildHTMLReport(AnalyzerType.CBO, data);
              //    history.Save(contant);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CalculateDependencyIssues(List<GraphNode> data)
        {

            //if absract class is should include only metheds 
            //If it include prop error 
           foreach(var root in data)
           {
               if(root.Childrens.Count > 0)
               {
                   foreach(var child in root.Childrens)
                   {
                       var childrens = data.Where(x => x.Name == child.Name).FirstOrDefault();
                       if (childrens != null && childrens.Childrens.Count > 0)
                       {
                           child.HasHiddenChildren = true;
                           child.Childrens.AddRange(childrens.Childrens);
                       }
                       else child.HasHiddenChildren = false;
                   }
               }
           }
        }

        private void AddArrows(List<BaseGraphType> data)
        {
            List<Edge> edges = new List<Edge>();
     
            foreach (var i in data)
            {
                if (i.GetType() == typeof(Node))
                {
                    var f = data.FirstOrDefault(x => x.label == i.label);
                    if (f != null)
                    {
                        Edge e = new Edge(i as Node, f as Node, "");
                        edges.Add(e);
                    }
                }
            }
            data.AddRange(edges);
        }

        public List<BaseGraphType> BuildRootGraph(List<GraphNode> nodes)
        {
            int id = 0;
            List<BaseGraphType> graph = new List<BaseGraphType>();
            foreach (var item in nodes)
            {
                Node node = new Node(id++, item.Name, item.dependencyType, item.nodeType,false);
                graph.Add(node);
                id = ExpandChild(graph, node, item, id);

            }


            return graph;
        }

        int ExpandChild (List<BaseGraphType> graph ,Node node , GraphNode source ,int id)
        {
            
            foreach (var child in source.Childrens)
            {
                Node childNode = new Node(id++, child.Name, child.dependencyType, child.nodeType, child.HasHiddenChildren);
                if (child.Childrens.Count() > 0)
                {
                    if(child.Childrens.Exists(x=>x.Name == source.Name))
                    {
                        Edge circle = new Edge(childNode, childNode, "circular dependency");
                        graph.Add(circle);
                    }
                }
                Edge edge = new Edge(node, childNode, "");
                graph.Add(childNode);
                graph.Add(edge);
               
            }
            return id;  
        }
    }
}
