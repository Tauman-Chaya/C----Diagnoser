using BL.Model;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    public interface IBLTypeGroup:IcrudBL<BLTypeGroup>
    {
        Task<string> Name(int code);
        Task<int> Code(string name);
        BLTypeGroup Convert(TypeGroup group);
        Task<TypeGroup> Convert(BLTypeGroup group);
    }
}
