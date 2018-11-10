using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeponencyAnalyzer.Engine.Interfaces
{
    public interface IHistory<T>
    {
         bool Save(T data);
    }
}
