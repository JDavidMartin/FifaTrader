using System.Collections.Generic;

namespace FifaTrader.Models.ModelBuilders
{
    public interface IPlayerIdModelBuilder
    {
        List<PlayerIdModel> ReadStoredPlayers();

        List<PlayerIdModel> AddPlayerIdToStore(string playerName, int playerId);

        List<PlayerIdModel> RemovePlayerFromStore(int index);
    }
}
