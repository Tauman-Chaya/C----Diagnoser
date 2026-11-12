using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Model
{
    public class BLReference
    {
        public int Code { get; set; }

        public int CodeWorkshop { get; set; }

        public int CodeCustomer { get; set; }

        public DateOnly Date { get; set; }

        public double Time { get; set; }

        public string Adress { get; set; } = null!;

        public string? Comments { get; set; }

        public int Status { get; set; }

    }
}
