using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using sm_coding_challenge.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace sm_coding_challenge.Services.DataProvider
{
    public class DataProviderImpl : IDataProvider
    {
        private const string CACHE_NAME = "playerData";
        private readonly IDistributedCache _playersCache;
        private readonly TimeSpan _timeout;

        public DataProviderImpl(IDistributedCache playersCache)
        {
            _playersCache = playersCache;
            _timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<DataResponseModel> FetchPlayerData()
        {
            var cachedData = await _playersCache.GetStringAsync(CACHE_NAME);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonConvert.DeserializeObject<DataResponseModel>(cachedData, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            }

            using (var client = new HttpClient { Timeout = _timeout })
            {
                var response = await client.GetAsync("https://gist.githubusercontent.com/RichardD012/a81e0d1730555bc0d8856d1be980c803/raw/3fe73fafadf7e5b699f056e55396282ff45a124b/basic.json");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to fetch player data. Response: {response.StatusCode} {response.ReasonPhrase}");
                }

                var stringData = response.Content.ReadAsStringAsync().Result;

                var dataResponse = JsonConvert.DeserializeObject<DataResponseModel>(stringData, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

                cachedData = JsonConvert.SerializeObject(dataResponse, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

                await _playersCache.SetStringAsync(CACHE_NAME, cachedData);

                return dataResponse;
            }
        }

        public async Task<ResponseModel> GetPlayerByIds(IEnumerable<string> id)
        {            
            var playerData  = await FetchPlayerData();

            ResponseModel response = new ResponseModel();

            response.Rushing.AddRange(playerData.Rushing.Where(x => id.Contains(x.Id)));
            response.Passing.AddRange(playerData.Passing.Where(x => id.Contains(x.Id)));
            response.Receiving.AddRange(playerData.Receiving.Where(x => id.Contains(x.Id)));
            response.Kicking.AddRange(playerData.Kicking.Where(x => id.Contains(x.Id)));

            return response;
        }

        /// <summary>
        /// I took this to mean to return whatever is in the cache, if it exists, and if not, to fetch the data from the source and then return it.
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseModel> GetLatestPlayers()
        {
            var playerData = await FetchPlayerData();

            ResponseModel response = new ResponseModel();

            response.Rushing.AddRange(playerData.Rushing);
            response.Passing.AddRange(playerData.Passing);
            response.Receiving.AddRange(playerData.Receiving);
            response.Kicking.AddRange(playerData.Kicking);

            return response;
        }
    }
}
