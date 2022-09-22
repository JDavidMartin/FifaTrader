using System.Collections.Generic;

namespace FifaTrader.Models.SearchOptions
{
    public class PlayerPositions
    {
        public List<string> Positions { get; set; } = new List<string>
        {
            "GK",
            "RWB",
            "RB",
            "CB",
            "LB",
            "RM",
            "CDM",
            "CM",
            "LM",
            "CAM",
            "RW",
            "LW",
            "ST",
            "CF"
        };
    }
}
