using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public partial class Workshop
    {
        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is Workshop))
                return false;
            Workshop w = ((Workshop)obj);
            return w.CodeDiagnoser == this.CodeDiagnoser &&
                w.TypeGroup == this.TypeGroup &&
                w.Description == this.Description;
        }
    }
}
