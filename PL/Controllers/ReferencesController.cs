using BL;
using BL.API;
using BL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace PL.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ReferencesController : ControllerBase
    {
        private IBLReference References;
        private IBLStatus Status;
        private IBLDiagnoser Diagnoser;
        private IBLWorkshop Workshop;

        public ReferencesController(BLManager bLManager)
        {
            References = bLManager.reference;
            Status = bLManager.status;
            Workshop = bLManager.workshop;
            Diagnoser = bLManager.diagnoser;
        }

        [HttpGet]
        public async Task<List<BLReference>> ReadAll()
        {
            return await References.Read();
        }

        [HttpGet]
        public async Task<BLReference> Read(int code)
        {
            return (await References.Read(r => r.Code == code)).FirstOrDefault();
        }

        [HttpPost]
        public async Task<BLReference> Add([FromBody] BLReference reference)
        {
            try
            {
                await References.Create(reference);
                List<BLReference> r = await ReadAll();
                return r[r.Count - 1];
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return null;
            }

        }

        [HttpDelete]
        public async Task Delete([FromBody] BLReference reference)
        {
            await References.Delete(reference);
        }

        [HttpPut]
        public async Task Update([FromBody] BLReference reference)
        {
            await References.Update(reference);
        }

        [HttpPut]
        [Route("{code}")]
        public async Task CloseOrder(int code)
        {
            await References.CloseOrder(code);
        }

        [HttpPut]
        [Route("{code}")]
        public async Task Pay(int code)
        {
            await References.Pay(code);
        }

        [HttpPut]
        public async Task DeleteOrder(int code)
        {
            await References.DeleteOrder(code);
        }

        [HttpGet]
        public async Task<BLWorkshop> GetWorkShop(int code)
        {
            return await References.Workshop(await Read(code));
        }

        [HttpGet]
        public async Task<BLDiagnoser> GetDiagnoser(int code)
        {
            return await References.Diagnoser(await Read(code));
        }
    }
}
