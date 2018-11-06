using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.Interfaces
{
    public interface IFileActions
    {
        ModuleDefinition LoadFile(string fileName);

        void PrintToFile(string path, string fileName, string contant);

        string ReadFile(string path);
        void CopyFolderTo(string source, string dest);

        DirectoryInfo CreateFolder(string path, string name);



           


    }
}
