using BL;
using BL.API;
using BL.Exeptions;
using BL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PL.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        IBLLead BlLeadController;
        IBLTypeGroup typeGroup;
        public LeadsController(BLManager bLManager)
        {
            BlLeadController = bLManager.lead;
            typeGroup = bLManager.typeGroup;
        }

        [HttpGet]
        public async Task<List<BLLead>> ReadAll()
        {
            return await BlLeadController.Read();
        }

        [HttpPost]
        public async Task<string> Type([FromBody] BLLead c)
        {
            return await typeGroup.Name(c.CodeType);
        }

        [HttpPost]
        public async Task<int> Add([FromBody] BLLead c)
        {
            try
            {
                await BlLeadController.Create(c);
                List<BLLead> l = ReadAll().Result;
                return l[l.Count - 1].Code;
            }
            catch(SignInException ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        [HttpDelete]
        public async Task Delete([FromBody] BLLead c)
        {
            try
            {
                await BlLeadController.Delete(c);
            }
            catch (SignInException ex)
            {
                Console.WriteLine(ex);
            }
        }

        [HttpPost]
        public async Task Update([FromBody] BLLead c)
        {
            try
            {
                await BlLeadController.Update(c);
            }
            catch (SignInException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
