using Dispetcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MyPlugin
{
    public class Plugin : IAddition
    {
        public List<string> OutputParams { get; set; }
        public string GeneralInfo { get; set; }
        public string AuthorInfo { get; set; }
        public int TimeToUpdateData { get; set; }
        public PerformanceCounter Cpucounter { get; set; } = new PerformanceCounter();
        public PerformanceCounter Memcounter { get; set; } = new PerformanceCounter();

        public void Do()
        {
            MainCode();
        }

        private void MainCode()
        {
            Cpucounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            Memcounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        }
        public string CPUUsage()
        {
            return Cpucounter.NextValue().ToString() + " %";
        }

        public string MemUsage()
        {
            return Memcounter.NextValue().ToString() + " %";
        }
    }
}
