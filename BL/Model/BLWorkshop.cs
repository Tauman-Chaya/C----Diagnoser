using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Model
{
    public class BLWorkshop
    {
        public int Code { get; set; }

        public int CodeDiagnoser { get; set; }

        public int TypeGroup { get; set; }

        public string? Description { get; set; }

        public bool Morfology { get; set; }

        public bool Grafology { get; set; }

        public bool Chirology { get; set; }

        public int Price { get; set; }

        public int? AccontOfPeople { get; set; }
    }
}
