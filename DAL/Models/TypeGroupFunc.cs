using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public partial class TypeGroup
    {
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if(obj is not TypeGroup)
                return false;
            if(((TypeGroup)obj).Code == Code)
                return true;
            return false;
        }
    }
}
