using System.Diagnostics;

namespace Dispetcher.Classes
{
    public class NewProces
    {
        public NewProces(string NameProceseInCorenFile)
        {
            StartNewProcess(NameProceseInCorenFile);
        }

        public void StartNewProcess(string Name)
        {
            var NewProces = Process.Start(Name);
        }
    }
}
