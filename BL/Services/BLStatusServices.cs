using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.API;
using BL.Model;
using DAL;
using DAL.Models;

namespace BL.Services
{
    public class BLStatusServices : IBLStatus
    {
        private List<Status> statuses;

        public BLStatusServices(DalManager manager)
        {
            // כאן ניתן למלא בנתונים סטטיים לדוגמה או למשוך אותם מה‐DAL
            statuses = manager.Status.Read().Result;
        }

        // מחזיר רשימה של BLStatus
        public async Task<List<BLStatus>> Read()
        {
      //System.AggregateException: 'One or more errors occurred. (An attempt to attach an auto-named database for file A:\שנה ב\#C\Morfology\Data\MyData.mdf failed. A database with the same name exists, or specified file cannot be opened, or it is located on UNC share.)'
      return statuses.Select(s => Convert(s)).ToList();
        }
        public async Task<List<BLStatus>> Read(Func<BLStatus, bool> func)
        {
            return (await Read()).Where(s => func(s)).ToList();
        }

        // מחזיר תיאור לפי קוד
        public async Task<string> Status(int code)
        {
            var s = statuses.FirstOrDefault(st => st.Code == code);
            return s?.Description ?? "לא ידוע";
        }

        // ממיר מ‐Status ל‐BLStatus
        public BLStatus Convert(Status status)
        {
            if (status == null) return null;
            return new BLStatus
            {
                Code = status.Code,
                Description = status.Description
            };
        }

        // ממיר מ‐BLStatus ל‐Status
        public Status Convert(BLStatus status)
        {
            if (status == null) return null;
            return new Status
            {
                Code = status.Code,
                Description = status.Description
            };
        }

    }
}
