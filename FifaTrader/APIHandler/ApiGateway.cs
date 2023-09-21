using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using FifaTrader.Models.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler
{
    public class ApiGateway : IApiGateway
    {
        private readonly IGetRequestHandler _getRequestHandler;
        private readonly IBidViewModelBuilder _modelBuilder;
        private readonly IPutRequestHandler _putRequestHandler;
        private readonly IPostRequestHandler _postRequestHandler;
        private readonly IDeleteHandler _deleteHandler;

        public ApiGateway(IGetRequestHandler getRequestHandler, IBidViewModelBuilder modelBuilder,
            IPutRequestHandler putRequestHandler, IPostRequestHandler postRequestHandler,
            IDeleteHandler deleteHandler)
        {
            _getRequestHandler = getRequestHandler;
            _modelBuilder = modelBuilder;
            _putRequestHandler = putRequestHandler;
            _postRequestHandler = postRequestHandler;
            _deleteHandler = deleteHandler;
        }

        public async Task<string> BidOnPlayer(string tradeId, int bidPrice, string accessToken)
        {
            var bidResponse = await _putRequestHandler.PutBidOnPlayer(tradeId, bidPrice, accessToken);
            return bidResponse;
        }

        public async Task<string> CheckToken(string accessToken)
        {
            var response = await _getRequestHandler.CheckToken(accessToken);
            return response;
        }

        public async Task ClearExpiredPlayers(string accessToken, List<BidViewModel> allPlayers)
        {
            await _deleteHandler.DeleteExpiredPlayers(accessToken, allPlayers);
        }

        public async Task<List<BidViewModel>> FetchPlayers(int playerId, int bidPrice, string accessToken)
        {
            var searchList = await _getRequestHandler.SearchForSpecificPlayer(playerId, bidPrice, accessToken);
            var defaultValue = new List<BidViewModel> {
                new BidViewModel
                {
                    Status = "expired",
                    TimeRemaining = -1,
                    BidPrice = 0,
                    Pending = false,
                    TradeId = "No Players Found"
                }
            };

            return searchList.Count == 0 ? defaultValue : searchList;
        }

        public async Task<List<BidViewModel>> FetchPlayersByLeague(int leagueId, int rarityId, int bidPrice,
            string accessToken, string positionId, int nationId)
        {
            var searchList = await _getRequestHandler.SearchForLeagueRarityPlayers(leagueId, rarityId, bidPrice, accessToken,
                positionId, nationId);
            var defaultValue = new List<BidViewModel> {
                new BidViewModel
                {
                    Status = "expired",
                    TimeRemaining = -1,
                    BidPrice = 0,
                    Pending = false,
                    TradeId = "No Players Found"
                }
            };

            return searchList.Count == 0 ? defaultValue : searchList;
        }

        public async Task<auctionSearchModel> GetTransferList(string accessToken)
        {
            try
            {
                var transferList = await _getRequestHandler.GetTransferList(accessToken);
                if (transferList.AuctionInfo?.Count == 0 || transferList.AuctionInfo == null)
                {
                    throw new Exception();
                }

                transferList.AuctionInfo = _modelBuilder.PopulateDefaultFieldsOfBidViews(transferList.AuctionInfo);
                return transferList;
            }
            catch (Exception ex)
            {
                return new auctionSearchModel
                {
                    AuctionInfo = new List<BidViewModel>
                    {
                        new BidViewModel
                        {
                            Status= "expired",
                            Pending = false
                        }
                    }
                };
            }
        }

        public async Task<auctionSearchModel> GetTransferTargets(string accessToken)
        {
            try
            {
                var targetsList = await _getRequestHandler.GetTransferTargets(accessToken);
                if (targetsList.AuctionInfo?.Count == 0 || targetsList.AuctionInfo == null)
                {
                    throw new Exception();
                }

                targetsList.AuctionInfo = _modelBuilder.PopulateDefaultFieldsOfBidViews(targetsList.AuctionInfo);
                return targetsList;
            }
            catch (Exception)
            {
                return new auctionSearchModel
                {
                    AuctionInfo = new List<BidViewModel>
                    {
                        new BidViewModel
                        {
                            Status= "expired",
                            Pending = false
                        }
                    }
                };
            }
        }

        public async Task<auctionSearchModel> GetUnassignedPile(string accessToken)
        {
            try
            {
                var targetsList = await _getRequestHandler.GetUnassignedPile(accessToken);
                if (targetsList.AuctionInfo?.Count == 0 || targetsList.AuctionInfo == null)
                {
                    throw new Exception();
                }

                targetsList.AuctionInfo = _modelBuilder.PopulateDefaultFieldsOfBidViews(targetsList.AuctionInfo);
                return targetsList;
            }
            catch (Exception)
            {
                return new auctionSearchModel
                {
                    AuctionInfo = new List<BidViewModel>
                    {
                        new BidViewModel
                        {
                            Status= "expired",
                            Pending = false
                        }
                    }
                };
            }
        }

        public async Task<string> SellPlayer(string tradeId, string playerId, string accessToken, int startPrice, int BinPrice)
        {
            var moveCode = await _putRequestHandler.MovePlayerToTradePile(tradeId, playerId, accessToken);
            if (moveCode == HttpStatusCode.OK)
            {
                //Sell player
                var response = await _postRequestHandler.SellPlayer(playerId, accessToken, startPrice, BinPrice);

                return response;
            }

            return "Error";
        }
    }
}
