using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface IReference:Icrud<Reference>
    {
        Workshop Workshop(Reference reference);
        Customer Customer(Reference reference);
        Status GetStatus(Reference reference);

    }
}
