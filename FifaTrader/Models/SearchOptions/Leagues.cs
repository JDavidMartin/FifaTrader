using System.Collections.Generic;

namespace FifaTrader.Models.SearchOptions
{
    public class Leagues
    {
        public Dictionary<string, string> LeagueOptions { get; set; } = new Dictionary<string, string>
        {
            {"Ligue 1", "16" },
            {"Premier League", "13" },
            {"Serie A", "31" },
            {"Bundesliga", "19" },
            {"La Liga", "53" }
        };
    }
}
