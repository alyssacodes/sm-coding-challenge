using sm_coding_challenge.Models;
using System.Collections.Generic;

namespace sm_coding_challenge.Models
{
    public class ResponseModel
    {
        public List<RushingModel> Rushing { get; set; } = new List<RushingModel>();
        public List<PassingModel> Passing { get; set; } = new List<PassingModel>();
        public List<ReceivingModel> Receiving { get; set; } = new List<ReceivingModel>();
        public List<KickingModel> Kicking { get; set; } = new List<KickingModel>();
    }
}
