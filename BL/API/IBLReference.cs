using BL.Model;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    public interface IBLReference:IcrudBL<BLReference>
    {
        BLReference Convert(Reference reference);
        Task<Reference> Convert(BLReference reference);
        Task<BLDiagnoser> Diagnoser(BLReference bLReference);
        Task<BLWorkshop> Workshop(BLReference bLReference);
        Task<BLCustomer> Customer(BLReference bLReference);
        Task<BLStatus> Status(BLReference bLReference);
        Task CloseOrder(int code);
        Task Pay(int code);
        Task DeleteOrder(int code);
    }
}
