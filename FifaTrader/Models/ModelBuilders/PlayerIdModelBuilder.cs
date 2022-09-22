using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace FifaTrader.Models.ModelBuilders
{
    public class PlayerIdModelBuilder : IPlayerIdModelBuilder
    {
        public List<PlayerIdModel> AddPlayerIdToStore(string playerName, int playerId)
        {
            var players = new List<PlayerIdModel>();
            var newPlayer = new PlayerIdModel
            {
                PlayerId = playerId,
                PlayerName = playerName
            };

            try
            {
                using (StreamReader r = new StreamReader("Data/PlayerIds.json"))
                {
                    string json = r.ReadToEnd();
                    players = JsonConvert.DeserializeObject<List<PlayerIdModel>>(json);
                    
                    players.Add(newPlayer);
                }
            }
            catch (Exception e)
            {
                return new List<PlayerIdModel>();
            }

            try
            {
                File.WriteAllText("Data/PlayerIds.json", JsonConvert.SerializeObject(players));
                return players;
            }
            catch(Exception e)
            {
                return new List<PlayerIdModel>();
            }
        }

        public List<PlayerIdModel> ReadStoredPlayers()
        {
            try
            {
                using (StreamReader r = new StreamReader("Data/PlayerIds.json"))
                {
                    string json = r.ReadToEnd();
                    List<PlayerIdModel> players = JsonConvert.DeserializeObject<List<PlayerIdModel>>(json);

                    return players;
                }
            }
            catch (Exception e)
            {
                return new List<PlayerIdModel>();
            }

        }

        public List<PlayerIdModel> RemovePlayerFromStore(int index)
        {
            var players = new List<PlayerIdModel>();

            try
            {
                using (StreamReader r = new StreamReader("Data/PlayerIds.json"))
                {
                    string json = r.ReadToEnd();
                    players = JsonConvert.DeserializeObject<List<PlayerIdModel>>(json);
                }
            }
            catch (Exception e)
            {
                return new List<PlayerIdModel>();
            }

            try
            {
                players.RemoveAt(index);
                File.WriteAllText("Data/PlayerIds.json", JsonConvert.SerializeObject(players));
                return players;
            }
            catch (Exception e)
            {
                return new List<PlayerIdModel>();
            }
        }
    }
}
