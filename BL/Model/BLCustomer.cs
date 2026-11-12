using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Model
{
    public class BLCustomer
    {
        public int Code { get; set; }
        public string Name { get; set; } = null!;

        public string Mail { get; set; } = null!;

        public int Password { get; set; }

        public string? Phone { get; set; }

        public int CodeType { get; set; }

    }
}
