using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exeptions
{
    public class SignInException : Exception
    {
        public SignInException(string s)
        {
            throw new Exception("לא הוכנס " + s + " מלא את הפרטים החסרים.");
        }

        public SignInException(bool b)
        {
            if (b)
                throw new Exception("קיימת מאבחנת עם מייל זהה במערכת זו");
            throw new Exception("אין אפשרות להרשם ללא הכנסת פרטים אישיים\nהכנס את פרטיך");
        }
    }
}
