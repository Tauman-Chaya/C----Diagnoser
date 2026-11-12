using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public partial class Customer
    {
        public override bool Equals(object obj)
        {
            if (obj is Customer)
                return Mail == ((Customer)(obj)).Mail;
            return false;
        }
    }
}
