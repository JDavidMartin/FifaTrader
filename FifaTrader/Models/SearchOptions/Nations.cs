using System.Collections.Generic;

namespace FifaTrader.Models.SearchOptions
{
    public class Nations
    {
        public Dictionary<string, int> NationOptions { get; set; } = new Dictionary<string, int>
        {
            {"England", 14 },
            {"France", 18 },
            {"Germany", 21 },
            {"Netherlands", 34 },
            {"Spain", 45  },
            {"Brazil", 54 },
            {"Argentina", 52 },
        };
    }
}
