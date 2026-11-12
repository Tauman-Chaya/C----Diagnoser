using BL.Exeptions;
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
    public interface IBLWorkshop : IcrudBL<BLWorkshop>
    {
        Task<String> Type(BLWorkshop w);
        Task<BLDiagnoser> Diagnoser(BLWorkshop w);
        Task<Workshop> Convert(BLWorkshop w);
        BLWorkshop Convert(Workshop w);
        Task<List<BLWorkshop>> GetWorkshops(string mail);
        Task<List<BLDiagnoser>> GetDiagnosers(bool M, bool G, bool C, int typeGroup);
        Task<BLWorkshop> GetLastWorkShop();
        Task<List<BLWorkshop>> GetWorkShops(bool M, bool G, bool C, int typeGroup);

    }
}
