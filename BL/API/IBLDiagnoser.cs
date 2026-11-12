using BL.Model;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    public interface IBLDiagnoser:IcrudBL<BLDiagnoser>
    {
        Task<bool> IsAvailable(BLDiagnoser d);
        Task<bool> IsMorfology(BLDiagnoser d);
        Task<bool> IsGrafology(BLDiagnoser d);
        Task<bool> IsChirology(BLDiagnoser d);
        BLDiagnoser Convert(Diagnoser d);
        Task<Diagnoser> Convert(BLDiagnoser d);
        Task<List<BLDiagnoser>> AllGrafology();
        Task<List<BLDiagnoser>> AllMorfology();
        Task<List<BLDiagnoser>> AllAvailable();
        Task<List<BLDiagnoser>> AllChirology();
        Task PayToManager(int code, int num);
        Task<List<BLDiagnoser>> GetDiagnosers(bool c, bool g, bool m);
    }
}
