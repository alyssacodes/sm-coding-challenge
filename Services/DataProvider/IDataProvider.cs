using sm_coding_challenge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sm_coding_challenge.Services.DataProvider
{
    public interface IDataProvider
    {       
        Task<ResponseModel> GetPlayerByIds(IEnumerable<string> ids);
        Task<ResponseModel> GetLatestPlayers();
    }
}
