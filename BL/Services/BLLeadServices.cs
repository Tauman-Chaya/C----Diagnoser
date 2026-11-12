using BL.API;
using BL.Exeptions;
using BL.Model;
using DAL;
using DAL.Api;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BLLeadServices : IBLLead
    {
        ILead Lead;

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="manager">הזרקות</param>
        public BLLeadServices(DalManager manager)
        {
            this.Lead = manager.Lead;
        }

        #region פונקציות

        #region המרות
        public BLLead Convert(Lead lead)
        {
            return new()
            {
                Code = lead.Code,
                Name = lead.Name,
                Mail = lead.Mail,
                CodeType = lead.CodeType,
                Password = lead.Password,
                Phone = lead.Phone
            };
        }

        public async Task<Lead> Convert(BLLead lead)
        {
            Lead c = (await Lead.Read()).Where(c => c.Mail == lead.Mail).FirstOrDefault();
            if (c != null)
                return c;
            return new()
            {
                CodeType = lead.CodeType,
                Password = lead.Password,
                Phone = lead.Phone,
                Name = lead.Name,
                Mail = lead.Mail,
            };
        }
        #endregion

        /// <summary>
        /// הוספת לקוח
        /// </summary>
        /// <param name="item">לקוח חדש</param>
        public async Task Create(BLLead item)
        {
            if (item == null)
                throw new SignInException(false);
            if (item.Mail == null)
                throw new SignInException("אימייל");
            if (item.Password == null)
                throw new SignInException("סיסמא");
            if (item.Name == null)
                throw new SignInException("שם");
            if ((await Read(d => d.Mail == item.Mail)).Count != 0)
                throw new SignInException(true);
            //if(item.CodeType == null)
            //    throw new SignInException("סוג");
            item.Mail = item.Mail.ToLower();
            await Lead.Create(await Convert(item));
        }

        /// <summary>
        /// מחיקת לקוח
        /// </summary>
        /// <param name="item">לקוח למחיקה</param>
        public async Task Delete(BLLead item)
        {
            await Lead.Delete(await Convert(item));
        }

        #region קריאות
        /// <summary>
        /// קריאה
        /// </summary>
        /// <returns>קבלת כל הלקוחות</returns>
        public async Task<List<BLLead>> Read()
        {
            List<BLLead> l = new();
            List<Lead> c = await Lead.Read();
            c.ForEach(c => l.Add(Convert(c)));
            return l;
        }

        /// <summary>
        /// קריאה על פי תנאי
        /// </summary>
        /// <param name="func">התנאי</param>
        /// <returns>קבלת הלקוחות שעונים על התנאי</returns>
        public async Task<List<BLLead>> Read(Func<BLLead, bool> func)
        {
            return (await Read()).Where(func).ToList();
        }
        #endregion

         /// <summary>
        /// קבלת סוג הלקוח
        /// </summary>
        /// <param name="customer">הלקוח</param>
        /// <returns>סוג הלקוח</returns>
        public async Task<string> Type(BLLead lead)
        {
            return await Lead.Type(await Convert(lead));
        }

        /// <summary>
        /// עדכון לקוח
        /// </summary>
        /// <param name="item">לקוח לעדכון</param>
        public async Task Update(BLLead item)
        {
            Lead c = await Convert(item);
            c.Name = item.Name;
            c.Password = item.Password;
            c.Mail = item.Mail;
            c.Phone = item.Phone;
            c.CodeType = item.CodeType;
            await Lead.Update(c);
        }
        #endregion
    }
}
