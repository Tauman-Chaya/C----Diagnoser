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
    public class BLCustomerServices : IBLCustomer
    {
        ICustomer customer;

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="manager">הזרקות</param>
        public BLCustomerServices(DalManager manager)
        {
            this.customer = manager.Customer;
        }

        #region פונקציות

        #region המרות
        public BLCustomer Convert(Customer cust)
        {
            return new()
            {
                Code = cust.Code,
                Name = cust.Name,
                Mail = cust.Mail,
                CodeType = cust.CodeType,
                Password = cust.Password,
                Phone = cust.Phone
            };
        }

        public async Task<Customer> Convert(BLCustomer cust)
        {
            Customer c = (await customer.Read()).Where(c => c.Mail == cust.Mail).FirstOrDefault();
            if (c != null)
                return c;
            return new()
            {
                CodeType = cust.CodeType,
                Password = cust.Password,
                Phone = cust.Phone,
                Name = cust.Name,
                Mail = cust.Mail,
            };
        }
        #endregion

        /// <summary>
        /// הוספת לקוח
        /// </summary>
        /// <param name="item">לקוח חדש</param>
        public async Task Create(BLCustomer item)
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
            await customer.Create(await Convert(item));
        }

        /// <summary>
        /// מחיקת לקוח
        /// </summary>
        /// <param name="item">לקוח למחיקה</param>
        public async Task Delete(BLCustomer item)
        {
            await customer.Delete(await Convert(item));
        }

        #region קריאות
        /// <summary>
        /// קריאה
        /// </summary>
        /// <returns>קבלת כל הלקוחות</returns>
        public async Task<List<BLCustomer>> Read()
        {
            List<BLCustomer> l = new();
            List<Customer> c = await customer.Read();
            c.ForEach(c => l.Add(Convert(c)));
            return l;
        }

        /// <summary>
        /// קריאה על פי תנאי
        /// </summary>
        /// <param name="func">התנאי</param>
        /// <returns>קבלת הלקוחות שעונים על התנאי</returns>
        public async Task<List<BLCustomer>> Read(Func<BLCustomer, bool> func)
        {
            return (await Read()).Where(func).ToList();
        }
        #endregion

         /// <summary>
        /// קבלת סוג הלקוח
        /// </summary>
        /// <param name="customer">הלקוח</param>
        /// <returns>סוג הלקוח</returns>
        public async Task<string> Type(BLCustomer cust)
        {
            return await customer.Type(await Convert(cust));
        }

        /// <summary>
        /// עדכון לקוח
        /// </summary>
        /// <param name="item">לקוח לעדכון</param>
        public async Task Update(BLCustomer item)
        {
            Customer c = await Convert(item);
            c.Name = item.Name;
            c.Password = item.Password;
            c.Mail = item.Mail;
            c.Phone = item.Phone;
            c.CodeType = item.CodeType;
            await customer.Update(c);
        }
        #endregion
    }
}
