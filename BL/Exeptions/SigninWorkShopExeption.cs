using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exeptions
{
    public class SigninWorkShopExeption : Exception
    {
        public SigninWorkShopExeption(string s, bool flag)
        {
            if (flag)
                throw new Exception("הוכנס " + s + " לא תקין");
            throw new Exception("לא הוכנס " + s + " מלא את הפרטים החסרים.");
        }

        public SigninWorkShopExeption()
        {
            throw new Exception("לא נשלחו פרטים תקינים");
        }
    }
}
