
using DeponencyAnalyzer.Engine.DTOs;
using DeponencyAnalyzer.Engine.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.Workers
{
    public class HtmlReport : IReportBuilder
    {
        IFileActions fileaction;
        public HtmlReport()
        {
            fileaction = new FileActions();
        }
        public string BuildHTMLReport(AnalyzerType type, List<BaseGraphType> data)
        {

            switch (type)
            {
                case AnalyzerType.CBO:
                  var dirInfo =  fileaction.CreateFolder(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CallGraphAnalyzer\\Reports", "CBO_" + DateTime.Now.Ticks.ToString());
                  fileaction.CopyFolderTo(Environment.CurrentDirectory + "\\Scripts", dirInfo.FullName);
                  fileaction.CopyFolderTo(Environment.CurrentDirectory + "\\Templatess\\CBO", dirInfo.FullName);
                  var contant = fileaction.ReadFile(dirInfo.FullName+"\\"+"CBO.html");
                  var nodes = data.Where(x => x.GetType() == typeof(Node)).ToList();
                  var edges = data.Where(x => x.GetType() == typeof(Edge)).ToList();
                  contant = contant.Replace("$nodes$", JsonConvert.SerializeObject(nodes));
                  contant = contant.Replace("$edges$", JsonConvert.SerializeObject(edges));
                  fileaction.PrintToFile(dirInfo.FullName, "CBO.html", contant);
                  System.Diagnostics.Process.Start("Chrome", Uri.EscapeDataString(dirInfo.FullName + "\\CBO.html"));
                   return contant;
                
                default:
                    return string.Empty;
            }


        }
        
    }
}
