using DAL.Api;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class WorkshopServices : IWorkshop
    {

        DbManager dbManager;

        public WorkshopServices(DbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public async Task Create(Workshop item)
        {
            dbManager.Workshops.Add(item);
            await dbManager.SaveChangesAsync();
        }

        public async Task Delete(Workshop item)
        {
            dbManager.Workshops.Remove(item);
            await dbManager.SaveChangesAsync();
        }

        public async Task<Diagnoser> Diagnoser(Workshop w)
        {
            List<Diagnoser> diagnosers = await dbManager.Diagnosers.Where(d => d.Code == w.CodeDiagnoser).ToListAsync();
            return diagnosers.First();
        }

        public async Task<List<Workshop>> Read()
        {
            return await dbManager.Workshops.ToListAsync();
        }

        public async Task<List<Workshop>> Read(Func<Workshop, bool> func)
        {
            List<Workshop> workshops = await Read();
            return workshops.Where(w=>func(w)).ToList();
        }

        public async Task<string> Type(Workshop w)
        {
            await foreach (var item in dbManager.TypeGroups)
            {
                if (w.TypeGroup == item.Code)
                    return item.Description;
            }
            return null;
        }

        public async Task Update(Workshop item)
        {
            dbManager.Workshops.Update(item);
            await dbManager.SaveChangesAsync();
        }
    }
}
