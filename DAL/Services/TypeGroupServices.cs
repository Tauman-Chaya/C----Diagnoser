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
    public class TypeGroupServices : ITypeGroup
    {
        DbManager dbManager;

        public TypeGroupServices(DbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public async Task Create(TypeGroup item)
        {
            dbManager.TypeGroups.Add(item);
            await dbManager.SaveChangesAsync();
        }

        public async Task Delete(TypeGroup item)
        {
            dbManager.TypeGroups.Remove(item);
            await dbManager.SaveChangesAsync();
        }

        public async Task<List<TypeGroup>> Read()
        {
            return await dbManager.TypeGroups.ToListAsync();
        }

        public async Task<List<TypeGroup>> Read(Func<TypeGroup, bool> func)
        {
            List<TypeGroup> typeGroups = await Read();
            return typeGroups.Where(t=>func(t)).ToList();
        }

        public async Task Update(TypeGroup item)
        {
            dbManager.TypeGroups.Update(item);
            await dbManager.SaveChangesAsync();
        }
    }
}
