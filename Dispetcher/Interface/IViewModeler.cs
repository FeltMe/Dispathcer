using Dispetcher.Classes;
using System.Collections.Generic;

namespace Dispetcher
{
	public interface IViewModeler
    {
        public List<MyAppHeader> AddApplication();
        public List<MyProcesessHeader> AddProcesess();
    }
}
