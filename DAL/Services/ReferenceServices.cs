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
    public class ReferenceServices : IReference
    {

        DbManager manager;
        public ReferenceServices(DbManager manager)
        {
            this.manager = manager;
        }

        //public async Task Create(Reference item)
        //{

        //    manager.References.Add(item);

        //        try { await manager.SaveChangesAsync(); }
        //        catch { manager.References.Local.Remove(item); }
        //}

        public async Task Create(Reference item)
        {
            var exists = manager.References.Local.Any(x => x == item);
            if (!exists)
            {
                manager.References.Add(item);
            }

            await manager.SaveChangesAsync();
        }

        public Customer Customer(Reference reference)
        {
            //await foreach (var item in manager.Customers)
            //    if (item.Code == reference.CodeCustomer)
            //        return item;
            //return null;
            return reference.CodeCustomerNavigation;
        }

        public async Task Delete(Reference item)
        {
            manager.References.Remove(item);
            await manager.SaveChangesAsync();
        }

        public Workshop Workshop(Reference reference)
        {
            //foreach (var item in manager.Diagnosers)
            //    if (item.Code == reference.CodeDiagnoser)
            //        return item;
            //return null;
            return reference.CodeWorkShopNavigation;
        }

        public Status GetStatus(Reference reference)
        {
            //foreach (var item in manager.Statuses)
            //    if (item.Code == reference.Status)
            //        return item;
            //return null;
            return reference.StatusNavigation;
        }

        public async Task<List<Reference>> Read()
        {
            return await manager.References.ToListAsync();
        }

        public async Task<List<Reference>> Read(Func<Reference, bool> func)
        {
            List<Reference> list = await Read();
            return list.Where(r => func(r)).ToList();
        }

        public async Task Update(Reference item)
        {
            manager.References.Update(item);
            await manager.SaveChangesAsync();
        }
    }
}
