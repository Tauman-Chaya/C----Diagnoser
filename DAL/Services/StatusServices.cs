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
    public class StatusServices : IStatus
    {
        DbManager dbManager;
        public StatusServices(DbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public async Task<List<Status>> Read()
        {
            return await dbManager.Statuses.ToListAsync();
        }
    }
}
