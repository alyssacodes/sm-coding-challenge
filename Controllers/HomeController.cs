using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sm_coding_challenge.Models;
using sm_coding_challenge.Services.DataProvider;

namespace sm_coding_challenge.Controllers
{
    public class HomeController : Controller
    {

        private IDataProvider _dataProvider;
        public HomeController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get players by ids
        /// Only one endpoint for all player requests to reduce code duplication.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Players(string ids)
        {
            try { 
                if (string.IsNullOrEmpty(ids))
                {
                    return BadRequest("No player ids provided");
                }

                // remove duplicate player ids
                var idList = ids.Split(',').Distinct();

                return Json(await _dataProvider.GetPlayerByIds(idList));
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", new {message = e.Message});
            }            
        }

        [HttpGet]
        public async Task<IActionResult> LatestPlayers()
        {
            try
            {
                return Json(await _dataProvider.GetLatestPlayers());
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", new {message = e.Message});
            }
        }

        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
        }
    }
}
