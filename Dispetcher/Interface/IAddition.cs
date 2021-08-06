using System.Collections.Generic;

namespace Dispetcher
{
	public interface IAddition
    {
        public void Do();
        public List<string> OutputParams { get; set; }
        public string GeneralInfo { get; set; }
        public string AuthorInfo { get; set; }
        public int TimeToUpdateData { get; set; }
    }
}
