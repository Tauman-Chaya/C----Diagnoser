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
    public class LeadServices : ILead
    {
        DbManager DbManager;

        #region c-tor - הזרקות
        public LeadServices(DbManager dbManager)
        {
            DbManager = dbManager;
        }
        #endregion

        #region פונקציות
        /// <summary>
        /// הוספת ליד חדש
        /// </summary>
        /// <param name="item">ליד חדש</param>
        public async Task Create(Lead item)
        {
            List<Lead> Leads = await Read();
            item.Code = Leads.Count == 0 ? 100
                : Leads[Leads.Count - 1].Code + 1;
            DbManager.Leads.Add(item);
            await DbManager.SaveChangesAsync();
        }

        /// <summary>
        /// מחיקת לקוח
        /// </summary>
        /// <param name="item">לקוח למחיקה</param>
        public async Task Delete(Lead item)
        {
            DbManager.Leads.Remove(item);
            await DbManager.SaveChangesAsync();
        }

        #region קריאות
        /// <summary>
        /// קריאה
        /// </summary>
        /// <returns>קבלת כל הלקוחות</returns>
        public async Task<List<Lead>> Read()
        {
            return await DbManager.Leads.ToListAsync();
        }

        /// <summary>
        /// קריאה על פי תנאי
        /// </summary>
        /// <param name="func">התנאי</param>
        /// <returns>קבלת הלקוחות שעונים על התנאי</returns>
        public async Task<List<Lead>> Read(Func<Lead, bool> func)
        {
            List<Lead> Leads = await Read();
            return Leads.Where(l => func(l)).ToList();
        }
        #endregion

        /// <summary>
        /// קבלת סוג הלקוח
        /// </summary>
        /// <param name="customer">הלקוח</param>
        /// <returns>סוג הלקוח</returns>
        public async Task<string> Type(Lead Lead)
        {
            await foreach (var item in DbManager.TypeGroups)
                if (item.Code == Lead.CodeType)
                    return item.Description;
            return null;
        }

        /// <summary>
        /// עדכון פרטי הלקוח
        /// </summary>
        /// <param name="item">לקוח לעדכון</param>
        public async Task Update(Lead item)
        {
            DbManager.Leads.Update(item);
            await DbManager.SaveChangesAsync();
        }
        #endregion
    }
}
