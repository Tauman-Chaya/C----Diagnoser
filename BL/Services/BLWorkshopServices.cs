using BL.API;
using BL.Exeptions;
using BL.Model;
using DAL;
using DAL.Api;
using DAL.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BLWorkshopServices : IBLWorkshop
    {

        IWorkshop workshop;
        IBLDiagnoser diagnoser;

        public BLWorkshopServices(DalManager dalManager, IBLDiagnoser diagnoser)
        {
            workshop = dalManager.Workshop;
            this.diagnoser = diagnoser;
        }

        public async Task<Workshop> Convert(BLWorkshop w)
        {
            if (w.Code != 0)
                return (await workshop.Read(wor => wor.Code==w.Code)).FirstOrDefault();
            return new()
            {
                CodeDiagnoser = w.CodeDiagnoser,
                Description = w.Description,
                Morfology = w.Morfology,
                Grafology = w.Grafology,
                Chirology = w.Chirology,
                Price = w.Price,
                TypeGroup = w.TypeGroup,
                AccontOfPeople = w.AccontOfPeople
            };
        }

        public BLWorkshop Convert(Workshop w)
        {
            return new()
            {
                Code = w.Code,
                CodeDiagnoser = w.CodeDiagnoser,
                Description = w.Description,
                Morfology= w.Morfology,
                Grafology = w.Grafology,
                Chirology = w.Chirology,
                Price = w.Price,
                TypeGroup = w.TypeGroup,
                AccontOfPeople = w.AccontOfPeople
            };
        }

        public async Task Create(BLWorkshop item)
        {
            if (item == null)
                throw new SigninWorkShopExeption();
            if (item.CodeDiagnoser == null)
                throw new SigninWorkShopExeption("מאבחנת", false);
            if (item.TypeGroup == null)
                throw new SigninWorkShopExeption("סוג קהל היעד", false);
            if (item.Morfology == null)
                throw new SigninWorkShopExeption("מורפולוגיה", false);
            if (item.Grafology == null)
                throw new SigninWorkShopExeption("גרפולוגיה", false);
            if (item.Chirology == null)
                throw new SigninWorkShopExeption("כירולוגיה", false);
            if (item.Price == 0)
                throw new SigninWorkShopExeption("מחיר", false);
            if ((await diagnoser.Read(d => d.Code == item.CodeDiagnoser)) == null)
                throw new SigninWorkShopExeption("פרטי מאבחנת", true);
            await workshop.Create(await Convert(item));
        }

        public async Task Delete(BLWorkshop item)
        {
            if (item == null) throw new SigninWorkShopExeption();
            if (item.Code == null) throw new SigninWorkShopExeption("קוד", false);
            if ((await Read(d => d.Code == item.Code)) == null)
                throw new SigninWorkShopExeption("פרטי קורס", true);
            await workshop.Delete(await Convert(item));
        }

        public async Task<BLDiagnoser> Diagnoser(BLWorkshop w)
        {
            return diagnoser.Convert(await workshop.Diagnoser(await Convert(w)));
        }

        public async Task<List<BLWorkshop>> Read()
        {
            List<BLWorkshop> list = new();
            List<Workshop> w = await workshop.Read();
            w.ForEach(w => list.Add(Convert(w)));
            return list;
        }

        public async Task<List<BLWorkshop>> Read(Func<BLWorkshop, bool> func)
        {
            return (await Read()).Where(x => func(x)).ToList();
        }

        public async Task<string> Type(BLWorkshop w)
        {
            return await workshop.Type(await Convert(w));
        }

        public async Task Update(BLWorkshop item)
        {
            Workshop w = await Convert(item);
            w.CodeDiagnoser = item.CodeDiagnoser;
            w.AccontOfPeople = item.AccontOfPeople;
            w.Price = item.Price;
            w.Description = item.Description;
            w.Grafology = item.Grafology;
            w.Morfology = item.Morfology;
            w.Chirology = item.Chirology;
            w.TypeGroup = item.TypeGroup;
            try
            {
                await workshop.Update(w);
            }
            catch
            {
                throw new Exception("לא קיים קורס כזה במערכת");
            }
        }

        public async Task<List<BLWorkshop>> GetWorkshops(string mail)
        {
            if ((await diagnoser.Read(d => d.Mail == mail)) == null)
                throw new SigninWorkShopExeption();
            int code = (await diagnoser.Read(d => d.Mail == mail)).FirstOrDefault().Code;
            return await Read(w => w.CodeDiagnoser == code);
        }

        public async Task<BLWorkshop> GetLastWorkShop()
        {
            List<BLWorkshop> workshops = await Read();
            return workshops[workshops.Count - 1];
        }

        public async Task<List<BLDiagnoser>> GetDiagnosers(bool M, bool G, bool C, int typeGroup)
        {
            List<BLDiagnoser> list = new();
            foreach (var item in await Read())
            {
                if (item.Morfology == M && item.Grafology == G && item.Chirology == C && item.TypeGroup == typeGroup)
                {
                    var diag = (await diagnoser.Read(d => d.Code == item.CodeDiagnoser)).FirstOrDefault();
                    if (diag != null && !list.Any(d => d.Code == diag.Code))
                    {
                        list.Add(diag);
                    }
                }
            }
            return list;
        }

        public async Task<List<BLWorkshop>> GetWorkShops(bool M, bool G, bool C, int typeGroup)
        {
            return await Read(w => w.Morfology == M && w.Grafology == G 
            && w.Chirology == C && w.TypeGroup == typeGroup);
        }


    }
}
