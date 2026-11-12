using BL;
using BL.API;
using BL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PL.Controllers
{


    [Route("[controller]/[action]")]
    [ApiController]
    public class DiagnosersController : ControllerBase
    {
        IBLDiagnoser BlDiagnoserController;
        public DiagnosersController(BLManager bLManager)
        {
            BlDiagnoserController = bLManager.diagnoser;
        }

        [HttpGet]
        public async Task<List<BLDiagnoser>> ReadAll()
        {
            return await BlDiagnoserController.Read();
        }

        [HttpPost]
        public async Task Add([FromBody] BLDiagnoser d)
        {
            await BlDiagnoserController.Create(d);
        }

        [HttpDelete]
        public async Task Delete([FromBody] BLDiagnoser d)
        {
            await BlDiagnoserController.Delete(d);
        }

        [HttpPost]
        public async Task Update([FromBody] BLDiagnoser d)
        {
            await BlDiagnoserController.Update(d);
        }

        [HttpPost]
        [Route("{num}/{code}")]
        public async Task PayToManager(int num, int code)
        {
            await BlDiagnoserController.PayToManager(code, num);
        }

        [HttpGet]
        public async Task<BLDiagnoser> Read(string Mail)
        {
            return (await BlDiagnoserController.Read(d => d.Mail == Mail)).First();
        }

        [HttpGet]
        [Route("{c}/{g}/{m}")]
        public async Task<List<BLDiagnoser>> GetDiagnosers(bool c, bool g, bool m)
        {
            return (await BlDiagnoserController.GetDiagnosers(c, g, m));
        }

        [HttpGet]
        public async Task<bool> IsMorfology(string mail)
        {
            return await BlDiagnoserController.IsMorfology(await Read(mail));
        }

        [HttpGet]
        public async Task<bool> IsGrafology(string mail)
        {
            return await BlDiagnoserController.IsGrafology(await Read(mail));
        }

        [HttpGet]
        public async Task<bool> IsChirology(string mail)
        {
            return await BlDiagnoserController.IsChirology(await Read(mail));
        }

        [HttpGet]
        public async Task<bool> IsAvailable(string mail)
        {
            return await BlDiagnoserController.IsAvailable(await Read(mail));
        }

        [HttpGet]
        public async Task<List<BLDiagnoser>> AllMorfology()
        {
            return await BlDiagnoserController.AllMorfology();
        }

        [HttpGet]
        public async Task<List<BLDiagnoser>> AllGrafology()
        {
            return await BlDiagnoserController.AllGrafology();
        }

        [HttpGet]
        public async Task<List<BLDiagnoser>> AllChirology()
        {
            return await BlDiagnoserController.AllChirology();
        }

        [HttpGet]
        public async Task<List<BLDiagnoser>> AllAvailable()
        {
            return await BlDiagnoserController.AllAvailable();
        }

    }
}
