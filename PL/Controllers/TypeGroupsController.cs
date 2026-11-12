using BL;
using BL.API;
using BL.Model;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PL.Controllers
{
    [Route("[action]")]
    [ApiController]
    public class TypeGroupsController : ControllerBase
    {
        IBLTypeGroup services;

        public TypeGroupsController(BLManager bLManager)
        {
            services = bLManager.typeGroup;
        }

        [HttpGet]
        public async Task<List<BLTypeGroup>> ReadAll()
        {
            return await services.Read();
        }

        [HttpGet("{name}")]
        public async Task<int> GetCode(string name)
        {
            return await services.Code(name);
        }

        [HttpGet("{code}")]
        public async Task<string> GetName(int code)
        {
            return await services.Name(code);
        }

        [HttpPost]
        public async Task Create([FromBody] BLTypeGroup typeGroup)
        {
            await services.Create(typeGroup);
        }

        [HttpPut]
        public async Task Update([FromBody] BLTypeGroup typeGroup)
        {
            await services.Update(typeGroup);
        }

        [HttpDelete]
        public async Task Delete([FromBody] BLTypeGroup typeGroup)
        {
            await services.Delete(typeGroup);
        }

    }
}
