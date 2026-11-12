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
    public interface IBLLead:IcrudBL<BLLead>
    {
        Task<string> Type(BLLead lead);
        BLLead Convert(Lead lead);
        Task<Lead> Convert(BLLead lead);
    }
}
