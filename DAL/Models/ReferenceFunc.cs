using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public partial class Reference
    {
        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Reference)
                return false;
            return ((Reference)obj).Code == this.Code;
        }
    }
}
