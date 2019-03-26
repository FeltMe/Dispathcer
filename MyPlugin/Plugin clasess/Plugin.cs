using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Dispetcher;

namespace MyPlugin
{
    public class Plugin : IAddition
    {
        public List<string> OutputParams { get; set; } = new List<string>();
        public string GeneralInfo { get; set; } = "My try to make some plugin";
        public string AuthorInfo { get; set; } = "Sashkoo";
        public int TimeToUpdateData { get; set; } = 5;
        public PerformanceCounter Cpucounter { get; set; } = new PerformanceCounter();
        public PerformanceCounter Memcounter { get; set; } = new PerformanceCounter();

        public void Do()
        {
            OutputParams.Add("Roma lox vse ykral, vorishka ne voryi!");
        }

        private void MainCode()
        {
            Cpucounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            Memcounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        }

        private string CPUUsage()
        {
            return Cpucounter.NextValue().ToString() + " %";
        }

        private string MemUsage()
        {
            return Memcounter.NextValue().ToString() + " %";
        }

        private void Refresh()
        {
            System.Timers.Timer t = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = TimeToUpdateData,
            };
            t.Elapsed += (a, b) =>
            {
                MainCode();
            };
            t.Start();
        }
    }
}
