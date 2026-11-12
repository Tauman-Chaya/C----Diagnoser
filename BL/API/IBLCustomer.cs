using BL.Model;
using DAL.Api;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    public interface IBLCustomer:IcrudBL<BLCustomer>
    {
        Task<string> Type(BLCustomer cust);
        BLCustomer Convert(Customer cust);
        Task<Customer> Convert(BLCustomer cust);
    }
}
