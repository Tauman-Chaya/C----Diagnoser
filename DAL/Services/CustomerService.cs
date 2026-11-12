using DAL.Models;
using DAL.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class CustomerService : ICustomer
    {
        DbManager DbManager;

        #region c-tor - הזרקות
        public CustomerService(DbManager dbManager)
        {
            DbManager = dbManager;
        }
        #endregion

        #region פונקציות
        /// <summary>
        /// הוספת לקוח חדש
        /// </summary>
        /// <param name="item">לקוח חדש</param>
        public async Task Create(Customer item)
        {
            List<Customer> customers = await Read();
            item.Code = customers.Count == 0 ? 100
                : customers[customers.Count - 1].Code + 1;
            DbManager.Customers.Add(item);
            await DbManager.SaveChangesAsync();
        }

        /// <summary>
        /// מחיקת לקוח
        /// </summary>
        /// <param name="item">לקוח למחיקה</param>
        public async Task Delete(Customer item)
        {
            DbManager.Customers.Remove(item);
            await DbManager.SaveChangesAsync();
        }

        #region קריאות
        /// <summary>
        /// קריאה
        /// </summary>
        /// <returns>קבלת כל הלקוחות</returns>
        public async Task<List<Customer>> Read()
        {
            return await DbManager.Customers.ToListAsync();
        }

        /// <summary>
        /// קריאה על פי תנאי
        /// </summary>
        /// <param name="func">התנאי</param>
        /// <returns>קבלת הלקוחות שעונים על התנאי</returns>
        public async Task<List<Customer>> Read(Func<Customer, bool> func)
        {
            List<Customer> customers = await Read();
            return customers.Where(c => func(c)).ToList();
        }
        #endregion

        /// <summary>
        /// קבלת סוג הלקוח
        /// </summary>
        /// <param name="customer">הלקוח</param>
        /// <returns>סוג הלקוח</returns>
        public async Task<string> Type(Customer customer)
        {
            await foreach (var item in DbManager.TypeGroups)
                if (item.Code == customer.CodeType)
                    return item.Description;
            return null;
        }

        /// <summary>
        /// עדכון פרטי הלקוח
        /// </summary>
        /// <param name="item">לקוח לעדכון</param>
        public async Task Update(Customer item)
        {
            DbManager.Customers.Update(item);
            await DbManager.SaveChangesAsync();
        }
        #endregion
    }
}
