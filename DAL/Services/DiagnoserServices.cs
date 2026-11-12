using DAL.Api;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class DiagnoserServices : IDiagnoser
    {
        DbManager dbManager;

        #region c-tor - הזרקות
        public DiagnoserServices(DbManager Manager)
        {
            dbManager = Manager;
        }
        #endregion

        #region פונקציות

        /// <summary>
        /// הוספת מאבחנת חדשה
        /// </summary>
        /// <param name="item">מאבחנת חדשה</param>
        public async Task Create(Diagnoser item)
        {
            dbManager.Diagnosers.Add(item);
            await dbManager.SaveChangesAsync();
        }

        /// <summary>
        /// מחיקת מאבחנת
        /// </summary>
        /// <param name="item">מאבחנת למחיקה</param>
        public async Task Delete(Diagnoser item)
        {
            dbManager.Diagnosers.Remove(item);
            await dbManager.SaveChangesAsync();
        }

        #region בדיקות על המאבחנות
        /// <summary>
        /// ?מאבחנת פנויה
        /// </summary>
        /// <param name="d">מאבחנת</param>
        /// <returns>?האם המאבחנת פנויה</returns>
        public bool IsAvailable(Diagnoser d)
        {
            return d.Available;
        }

        /// <summary>
        /// ?מאבחנת גרפולוגית
        /// </summary>
        /// <param name="d">מאבחנת</param>
        /// <returns>?האם המאבחנת גרפולוגית</returns>
        public bool IsGrafology(Diagnoser d)
        {
            return d.Graphology;
        }

        /// <summary>
        /// ?מאבחנת גרפולוגית
        /// </summary>
        /// <param name="d">מאבחנת</param>
        /// <returns>?האם המאבחנת גרפולוגית</returns>
        public bool IsChirology(Diagnoser d)
        {
            return d.Chirology;
        }

        /// <summary>
        /// ?מאבחנת מורפולוגית
        /// </summary>
        /// <param name="d">מאבחנת</param>
        /// <returns>?האם המאבחנת מורפולוגית</returns>
        public bool IsMorfology(Diagnoser d)
        {
            return d.Morphology;
        }
        #endregion

        #region קריאות
        /// <summary>
        /// קריאה
        /// </summary>
        /// <returns>קבלת כל המאבחנות</returns>
        public async Task<List<Diagnoser>> Read()
        {
            return await dbManager.Diagnosers.ToListAsync();
        }

        /// <summary>
        /// קריאה על פי תנאי
        /// </summary>
        /// <param name="func">התנאי</param>
        /// <returns>קבלת המאבחנות שעונות על התנאי</returns>
        public async Task<List<Diagnoser>> Read(Func<Diagnoser, bool> func)
        {
            List<Diagnoser> diagnosers = await Read();
            return diagnosers.Where(d => func(d)).ToList();
        }
        #endregion


        /// <summary>
        /// עדכון פרטי מאבחנת
        /// </summary>
        /// <param name="item">פרטי המאבחנת המעודכנים</param>
        public async Task Update(Diagnoser item)
        {
            dbManager.Diagnosers.Update(item);
            await dbManager.SaveChangesAsync();
        }
        #endregion
    }
}
