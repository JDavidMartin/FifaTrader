﻿using FifaTrader.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IGetRequestHandler
    {
        Task<List<BidViewModel>> SearchForSpecificPlayer(int playerId, int bidPrice, string accessToken);

        Task<List<BidViewModel>> SearchForLeagueRarityPlayers(int leagueId, int rarityId, int bidPrice, string accessToken, string positionId, int nationId);

        Task<auctionSearchModel> GetTransferTargets(string accessToken);

        Task<auctionSearchModel> GetTransferList(string accessToken);

        Task<auctionSearchModel> GetUnassignedPile(string accessToken);

        Task<string> CheckToken(string accessToken);
    }
}
