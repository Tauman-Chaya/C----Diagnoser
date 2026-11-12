using BL;
using BL.API;
using BL.Exeptions;
using BL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        IBLStatus BLStatusController;
        public StatusesController(BLManager bLManager)
        {
            BLStatusController = bLManager.status;
        }

        [HttpGet]
        public async Task<List<BLStatus>> ReadAll()
        {
            return await BLStatusController.Read();
        }

    }
}
