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
    public class CustomersController : ControllerBase
    {
        IBLCustomer BlCustomerController;
        IBLTypeGroup typeGroup;
        public CustomersController(BLManager bLManager)
        {
            BlCustomerController = bLManager.customer;
            typeGroup = bLManager.typeGroup;
        }

        [HttpGet]
        public async Task<List<BLCustomer>> ReadAll()
        {
            return await BlCustomerController.Read();
        }

        [HttpPost]
        public async Task<string> Type([FromBody] BLCustomer c)
        {
           
            return await typeGroup.Name(c.CodeType);
        }

        [HttpPost]
        public async Task<int> Add([FromBody] BLCustomer c)
        {
            try
            {
                await BlCustomerController.Create(c);
                List<BLCustomer> list = await ReadAll();
                return list[list.Count - 1].Code;
            }
            catch(SignInException ex)
            {
                Console.WriteLine(ex);
            }
            return 0;
        }

        [HttpDelete]
        public async Task Delete([FromBody] BLCustomer c)
        {
            try
            {
                await BlCustomerController.Delete(c);
            }
            catch (SignInException ex)
            {
                Console.WriteLine(ex);
            }
        }

        [HttpPost]
        public async Task Update([FromBody] BLCustomer c)
        {
            try
            {
                await BlCustomerController.Update(c);
            }
            catch (SignInException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
