using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public partial class Course
    {
        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Course)
                return false;
            return ((Course)obj).Code == this.Code;
        }
    }
}
