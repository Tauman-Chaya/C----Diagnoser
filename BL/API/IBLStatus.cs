using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Model;
using DAL.Models;

namespace BL.API
{
    public interface IBLStatus
    {
        Task <List<BLStatus>> Read();
        Task<List<BLStatus>> Read(Func<BLStatus, bool> func);
        Task<string> Status(int code);
        BLStatus Convert(Status status);
        Status Convert(BLStatus status);
    }
}
