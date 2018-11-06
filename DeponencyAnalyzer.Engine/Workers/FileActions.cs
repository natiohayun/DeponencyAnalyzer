
using DeponencyAnalyzer.Engine.Interfaces;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallGraphAnalyzer.Engine.Workers
{
    public class FileActions : IFileActions
    {
        public void CopyFolderTo(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolderTo(folder, dest);
            }
        }

        public DirectoryInfo CreateFolder(string path, string name)
        {
           return Directory.CreateDirectory(path + "\\" + name);
        }

        public ModuleDefinition LoadFile(string fileName)
        {
            try
            {
                return ModuleDefinition.ReadModule(fileName);
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public void PrintToFile(string path,string fileName, string contant)
        {

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, fileName)))
            {

                outputFile.WriteLine(contant);
            }
        }

        public string ReadFile(string path)
        {
            try
            {
                if (File.Exists( path))
                {
                    return File.ReadAllText(path);
                }
                else throw new Exception("File dont exist");
            }
            catch(Exception ex) { throw ex; }
        }
    }
}
