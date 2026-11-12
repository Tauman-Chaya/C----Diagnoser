using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exeptions
{
    public class ReferenceException : Exception
    {
        public ReferenceException(string error)
        {
            throw new Exception("לא קיים " + error + " במערכת");
        }

        public ReferenceException(DateOnly date)
        {
            throw new Exception("תאריך אינו תקין: " + date);
        }

        public ReferenceException(double Time)
        {
            throw new Exception((int)(Time) + ":" + Time * 100 % 100 + " שעה זו אינה תקינה");
        }
    }
}
