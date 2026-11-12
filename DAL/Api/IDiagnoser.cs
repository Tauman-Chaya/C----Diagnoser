using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface IDiagnoser:Icrud<Diagnoser>
    {
        bool IsAvailable(Diagnoser d);
        bool IsMorfology(Diagnoser d);
        bool IsGrafology(Diagnoser d);
        bool IsChirology(Diagnoser d);
    }
}
