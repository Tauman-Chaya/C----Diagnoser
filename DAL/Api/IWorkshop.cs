using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface IWorkshop:Icrud<Workshop>
    {
        Task<String> Type(Workshop w);
        Task<Diagnoser> Diagnoser(Workshop w);
    }
}
