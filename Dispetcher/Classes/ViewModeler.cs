using System.Collections.Generic;
using System.Diagnostics;
using Dispetcher.Classes;

namespace Dispetcher
{
    public class ViewModeler : IViewModeler
    {
        private readonly List<MyAppHeader> appsList = new();
        private readonly List<MyProcesessHeader> procesList = new();

        public List<MyAppHeader> AddApplication()
        {
            appsList.Clear();
            foreach (var item in Process.GetProcesses())
            {
                if(string.IsNullOrEmpty(item.MainWindowTitle) == false)
                {
                    MyAppHeader temp = new()
					{
                        AppNamee = item.ProcessName
                    };

                    appsList.Add(temp);
                }
            }
            return appsList;
        }

        public List<MyProcesessHeader> AddProcesess()
        {
            procesList.Clear();
            foreach (var item in Process.GetProcesses())
            {
                MyProcesessHeader TempMyProceses = new()
				{
                    Id = item.Id,
                    Name = item.ProcessName,
                    Priority = item.BasePriority,
                    Descripts = item.HandleCount
                };

                procesList.Add(TempMyProceses);
            }
            return procesList;
        }
    }
}
