using BL;
using BL.API;
using BL.Exeptions;
using BL.Model;
using DAL.Api;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkShopsController : ControllerBase
    {
        IBLWorkshop Workshops;
        IBLTypeGroup TypeGroups;

        public WorkShopsController(BLManager manager)
        {
            Workshops = manager.workshop;
            TypeGroups = manager.typeGroup;
        }

        [HttpGet]
        public async Task<List<BLWorkshop>> ReadAll()
        {
            return await Workshops.Read();
        }

        [HttpGet("{code}")]
        public async Task<BLWorkshop> Read(int code)
        {
            return (await Workshops.Read(w => w.Code == code)).FirstOrDefault();
        }

        [HttpGet("{mail}")]
        public async Task<List<BLWorkshop>> Get(string mail)
        {
            return await Workshops.GetWorkshops(mail);
        }

        [HttpGet("{m}/{g}/{c}/{typeGroup}")]
        public async Task<List<BLDiagnoser>> GetLDiagnosers(bool m, bool g, bool c, int typeGroup)
        {
            return await Workshops.GetDiagnosers(m, g, c, typeGroup);
        }

        [HttpGet("{m}/{g}/{c}/{typeGroup}")]
        public async Task<List<BLWorkshop>> GetWorkShops(bool m, bool g, bool c, int typeGroup)
        {
            return await Workshops.GetWorkShops(m, g, c, typeGroup);
        }


        [HttpPost]
        public async Task<int> Add([FromBody] BLWorkshop workshop)
        {
            await Workshops.Create(workshop);
            return (await Workshops.GetLastWorkShop()).Code;
        }

        [HttpPut]
        public async Task Update([FromBody] BLWorkshop workshop)
        {
            await Workshops.Update(workshop);
        }

        [HttpDelete]
        public async Task Delete([FromBody] BLWorkshop workshop)
        {
            await Workshops.Delete(workshop);
        }

        [HttpPost]
        public async Task<string> Type([FromBody] BLWorkshop workshop)
        {
            return await TypeGroups.Name(workshop.TypeGroup);
        }

    }
}
