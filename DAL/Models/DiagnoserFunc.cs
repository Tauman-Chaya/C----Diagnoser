using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public partial class Diagnoser
    {
        public override bool Equals(object? obj)
        {
            if (obj is Diagnoser)
                return Mail == ((Diagnoser)(obj)).Mail;
            return false;
        }
    }
}
