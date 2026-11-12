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
    public class BLDiagnoserServices : IBLDiagnoser
    {
        IDiagnoser diagnoser { get; }
        IWorkshop workshops { get; }

        #region c-tor - הזרקות
        public BLDiagnoserServices(DalManager dalManager, IWorkshop workshops)
        {
            diagnoser = dalManager.Diagnoser;
            this.workshops = workshops;
        }
        #endregion

        #region פונקציות

        #region בדיקות על המאבחנות
        /// <summary>
        /// ?מאבחנת פנויה
        /// </summary>
        /// <param name="d">מאבחנת</param>
        /// <returns>?האם המאבחנת פנויה</returns>
        public async Task<bool> IsAvailable(BLDiagnoser d)
        {
            return diagnoser.IsAvailable(await Convert(d));
        }

        /// <summary>
        /// ?מאבחנת מורפולוגית
        /// </summary>
        /// <param name="d">מאבחנת</param>
        /// <returns>?האם המאבחנת מורפולוגית</returns>
        public async Task<bool> IsMorfology(BLDiagnoser d)
        {
            return diagnoser.IsMorfology(await Convert(d));
        }

        /// <summary>
        /// ?מאבחנת גרפולוגית
        /// </summary>
        /// <param name="d">מאבחנת</param>
        /// <returns>?האם המאבחנת גרפולוגית</returns>
        public async Task<bool> IsGrafology(BLDiagnoser d)
        {
            return diagnoser.IsGrafology(await Convert(d));
        }

        /// <summary>
        /// ?מאבחנת גרפולוגית
        /// </summary>
        /// <param name="d">מאבחנת</param>
        /// <returns>?האם המאבחנת גרפולוגית</returns>
        public async Task<bool> IsChirology(BLDiagnoser d)
        {
            return diagnoser.IsChirology(await Convert(d));
        }
        #endregion

        #region המרה
        public BLDiagnoser Convert(Diagnoser d)
        {
            return new()
            {
                Code = d.Code,
                Available = d.Available,
                Graphology = d.Graphology,
                Mail = d.Mail,
                Morphology = d.Morphology,
                Name = d.Name,
                Password = d.Password,
                Phone = d.Phone,
                PrecentagePayment = d.PrecentagePayment,
                Chirology = d.Chirology,
            };
        }

        public async Task<Diagnoser> Convert(BLDiagnoser d)
        {
            Diagnoser diag = (await diagnoser.Read()).Where(di=>di.Mail == d.Mail).FirstOrDefault();
            if (diag != null)
                return diag;
            return new()
            {
                Available = d.Available,
                Graphology = d.Graphology,
                Mail = d.Mail,
                Morphology = d.Morphology,
                Name = d.Name,
                Password = d.Password,
                Phone = d.Phone,
                PrecentagePayment = d.PrecentagePayment,
                Chirology= d.Chirology,
            };
        }
        #endregion

        /// <summary>
        /// הוספת מאבחנת חדשה
        /// </summary>
        /// <param name="item">המאבחנת החדשה</param>
        /// <exception cref="SignInException">שגיאה בפרטי המאבחנת הנשלחים </exception>
        public async Task Create(BLDiagnoser item)
        {
            if (item == null)
                throw new SignInException(false);
            if (item.Mail == null)
                throw new SignInException("אימייל");
            if (item.Available == null)
                throw new SignInException("אם את פנויה");
            if (item.Graphology == null || item.Morphology == null || item.Chirology==null)
                throw new SignInException("תחום עבודתך:מורפולוגיה/גרפולוגיה/כירולוגיה");
            if (item.Password == null)
                throw new SignInException("סיסמא");
            if (item.Name == null)
                throw new SignInException("שם");
            if ((await Read(d => d.Mail == item.Mail)).Count != 0)
                throw new SignInException(true);
            item.PrecentagePayment = 0;
            item.Mail = item.Mail.ToLower();
            diagnoser.Create(await Convert(item));
        }

        #region קריאות
        /// <summary>
        /// קריאה 
        /// </summary>
        /// <returns>קבלת המאבחנות</returns>
        public async Task<List<BLDiagnoser>> Read()
        {
            List<BLDiagnoser> list = new();
            List<Diagnoser> d = await diagnoser.Read();
            d.ForEach(d => list.Add(Convert(d)));
            return list;
        }

        /// <summary>
        /// קריאה על פי תנאי
        /// </summary>
        /// <param name="func">התנאי</param>
        /// <returns>קבלת המאבחנות שעונות על התנאי</returns>
        public async Task<List<BLDiagnoser>> Read(Func<BLDiagnoser, bool> func)
        {
            return (await Read()).Where(d => func(d)).ToList();
        }
        #endregion

        /// <summary>
        /// עדכון פרטי מאבחנת
        /// </summary>
        /// <param name="item">פרטי המאבחנת המעודכנים</param>
        public async Task Update(BLDiagnoser item)
        {
            Diagnoser d = await Convert(item);
            d.Name = item.Name;
            d.Password = item.Password;
            d.Mail = item.Mail;
            d.Phone = item.Phone;
            d.Graphology = item.Graphology;
            d.Morphology = item.Morphology;
            d.Chirology = item.Chirology;
            d.Available = item.Available;
            d.PrecentagePayment = item.PrecentagePayment;
            List<Workshop> workshops = await this.workshops.Read(w => w.CodeDiagnoser == item.Code);
            bool flag = true;
            foreach (var workshop in workshops)
            {
                if (workshop.Grafology && !item.Graphology)
                    workshop.Grafology = flag = false;
                if (workshop.Morfology && !item.Morphology)
                    workshop.Morfology = flag = false;
                if (workshop.Chirology && !item.Chirology)
                    workshop.Chirology = flag = false;
                if (!(workshop.Chirology || workshop.Morfology || workshop.Grafology))
                    this.workshops.Delete(workshop);
                else if (!flag)
                    this.workshops.Update(workshop);
            }
            try
            {
                await diagnoser.Update(d);
            }
            catch (Exception ex)
            {
                throw new Exception("לא קיימת מאבחנת כזו במערכת זו");
            }
        }

        /// <summary>
        /// מחיקת מאבחנת
        /// </summary>
        /// <param name="item">מאבחנת למחיקה</param>
        public async Task Delete(BLDiagnoser item)
        {
            if (item == null)
                throw new Exception("לא הוכנסו פרטים למחיקה");
            try
            {
                List<Workshop> l = await workshops.Read();
                foreach (var w in l)
                {
                    if(w.CodeDiagnoser==item.Code)
                        await workshops.Delete(w);
                }
                await diagnoser.Delete(await Convert(item));
            }
            catch (Exception ex)
            {
                throw new Exception("לא קיימת מאבחנת כזו במערכת זו");
            }
        }

        public async Task<List<BLDiagnoser>> AllGrafology()
        {
            return await Read(d => IsGrafology(d).Result);
        }

        public async Task<List<BLDiagnoser>> AllMorfology()
        {
            return await Read(d => IsMorfology(d).Result);
        }

        public async Task<List<BLDiagnoser>> AllChirology()
        {
            return await Read(d => IsChirology(d).Result);
        }

        public async Task<List<BLDiagnoser>> AllAvailable()
        {
            return await Read(d => IsAvailable(d).Result);
        }

        public async Task PayToManager(int code, int num)
        {
            BLDiagnoser d = (await Read(d => d.Code == code)).FirstOrDefault();
            d.PrecentagePayment -= num;
            await Update(d);
        }

        public async Task<List<BLDiagnoser>> GetDiagnosers(bool c, bool g, bool m)
        {
            List<BLDiagnoser> list = await Read();
            if (c)
                list=list.Where(d => d.Chirology).ToList();
            if (m)
                list = list.Where(d => d.Morphology).ToList();
            if (g)
                list = list.Where(d => d.Graphology).ToList();
            return list;
        }
        #endregion
    }
}
