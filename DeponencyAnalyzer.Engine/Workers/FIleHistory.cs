using DeponencyAnalyzer.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.Workers
{
   public class FileHistory : IHistory<string>
    {
        public bool Save(string data)
        {
            try
            {
               var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CallGraphAnalyzer\\Data";
               if (!Directory.Exists(basePath))
                   Directory.CreateDirectory(basePath);

               using (StreamWriter outputFile = new StreamWriter(Path.Combine(basePath, "CBO_Data" + DateTime.Now.Ticks.ToString())))
               {

                   outputFile.WriteLine(data);
               }

               return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
