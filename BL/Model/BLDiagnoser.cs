using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Model
{
    public class BLDiagnoser
    {
        public int Code { get; set; }
        public string Name { get; set; } = null!;

        public string Mail { get; set; } = null!;

        public int Password { get; set; }

        public string? Phone { get; set; }

        public bool Morphology { get; set; }

        public bool Graphology { get; set; }

        public bool Chirology { get; set; }

        public bool Available { get; set; }

        public int? PrecentagePayment { get; set; }

    }
}
