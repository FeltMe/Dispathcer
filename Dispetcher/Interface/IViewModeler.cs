using Dispetcher.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispetcher
{
    public interface IViewModeler
    {
        List<MyAppHeader> AddApplication();
        List<MyProcesessHeader> AddProcesess();
    }
}
