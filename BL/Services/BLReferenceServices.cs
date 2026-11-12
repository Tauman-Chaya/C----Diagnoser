using BL.API;
using DAL.Api;
using BL.Model;
using BL.Exeptions;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Services
{
    public class BLReferenceServices : IBLReference
    {

        IReference References;
        IBLCustomer Customers;
        IBLDiagnoser Diagnosers;
        IBLWorkshop Workshops;
        IBLStatus Statuses;

        public BLReferenceServices(DalManager dalManager, IBLDiagnoser diagnoser, IBLCustomer customer, IBLStatus status, IBLWorkshop workshop)
        {
            this.References = dalManager.Reference;
            this.Customers = customer;
            this.Diagnosers = diagnoser;
            this.Statuses = status;
            this.Workshops = workshop;
        }

        public BLReference Convert(Reference reference)
        {
            return new()
            {
                Code = reference.Code,
                Date = reference.Date,
                CodeCustomer = reference.CodeCustomer,
                CodeWorkshop = reference.CodeWorkShop,
                Status = reference.Status,
                Time = reference.Time,
                Adress = reference.Adress,
                Comments = reference.Comments,
            };
        }

        public async Task<Reference> Convert(BLReference reference)
        {
            if (reference.Code != 0)
                return (await References.Read(re => re.Code == reference.Code)).FirstOrDefault();
            return new()
            {
                CodeWorkShop = reference.CodeWorkshop,
                CodeCustomer = reference.CodeCustomer,
                Status = reference.Status,
                Date = reference.Date,
                Time = reference.Time,
                Adress = reference.Adress,
                Comments = reference.Comments,
            };
        }

        public async Task Create(BLReference item)
        {
            if ((await Customers.Read(c => c.Code == item.CodeCustomer)).Count == 0)
                throw new ReferenceException("לקוח");
            if ((await Statuses.Read()).Where(s => s.Code == item.Status).ToList().Count == 0)
                throw new ReferenceException("סטטוס");
            if (item.Date.Year < DateTime.Now.Year ||
                (item.Date.Year == DateTime.Now.Year &&
                item.Date.Month < DateTime.Now.Month) ||
                (item.Date.Year == DateTime.Now.Year &&
                item.Date.Month == DateTime.Now.Month &&
                item.Date.Day < DateTime.Now.Day))
                throw new ReferenceException(item.Date);
            if (item.Time > 24 ||
                item.Time * 100 % 100 > 59)
                throw new ReferenceException(item.Time);
            await References.Create(await Convert(item));
        }

        public async Task<BLCustomer> Customer(BLReference bLReference)
        {
            return Customers.Convert(References.Customer(await Convert(bLReference)));
        }

        public async Task Delete(BLReference item)
        {
            if (item == null) throw new SigninWorkShopExeption();
            if (item.Code == null) throw new SigninWorkShopExeption("קוד", false);
            await References.Delete(await Convert(item));
        }

        public async Task<BLDiagnoser> Diagnoser(BLReference bLReference)
        {
            BLWorkshop workshop = await Workshop(bLReference);
            return await Workshops.Diagnoser(workshop);
        }

        public async Task<BLWorkshop> Workshop(BLReference bLReference)
        {
            return (await Workshops.Read(w => w.Code == bLReference.CodeWorkshop)).FirstOrDefault();
        }

        public async Task<List<BLReference>> Read()
        {
            List<BLReference> list = new();
            List<Reference> r = await References.Read();
            r.ForEach(r => list.Add(Convert(r)));
            return list;
        }

        public async Task<List<BLReference>> Read(Func<BLReference, bool> func)
        {
            return (await Read()).Where(r => func(r)).ToList();
        }

        public async Task<BLStatus> Status(BLReference bLReference)
        {
            return Statuses.Convert(References.GetStatus(await Convert(bLReference)));
        }

        public async Task Update(BLReference item)
        {
            Reference r = await Convert(item);
            r.CodeCustomer = item.CodeCustomer;
            r.CodeWorkShop = item.CodeWorkshop;
            r.Status = item.Status;
            r.Date = item.Date;
            r.Time = item.Time;
            r.Adress = item.Adress;
            r.Comments = item.Comments;
            try
            {
                await References.Update(r);
            }
            catch (Exception ex)
            {
                throw new Exception("לא קיימת הפניה כזו במערכת זו");
            }
        }

        public async Task CloseOrder(int code)
        {
            BLStatus s = (await Statuses.Read(r => r.Description.Trim() == "לא שולם")).FirstOrDefault();
            BLReference r = (await Read(r => r.Code == code)).FirstOrDefault();
            r.Status = s.Code;
            BLDiagnoser diag = await Diagnoser(r);
            if (diag.Mail.Trim() != "esty@2024")
                diag.PrecentagePayment += (int)((await Workshop(r)).Price * 0.1);
            await Diagnosers.Update(diag);
            await Update(r);
        }

        public async Task Pay(int code)
        {
            BLReference r = (await Read(r => r.Code == code)).FirstOrDefault();
            r.Status = (await Statuses.Read(r => r.Description.Trim() == "שולם")).FirstOrDefault().Code;
            await Update(r);
        }

        public async Task DeleteOrder(int code)
        {
            BLReference r = (await Read(r => r.Code == code)).FirstOrDefault();
            r.Status = (await Statuses.Read(r => r.Description.Trim() == "בוטל")).FirstOrDefault().Code;
            await Update(r);
        }


        //מהבינה


        /*
         * public class BLReferenceServices : IBLReference
        {
        private readonly IReference References;
        private readonly IBLCustomer Customers;
        private readonly IBLDiagnoser Diagnosers;
        private readonly IBLStatus Statuses;

        public BLReferenceServices(DalManager dalManager, IBLDiagnoser diagnoser, IBLCustomer customer, IBLStatus status)
        {
        References = dalManager.Reference;
        Customers = customer;
        Diagnosers = diagnoser;
        Statuses = status;
        }

        public BLReference Convert(Reference reference)
        {
        return new BLReference
        {
            Code = reference.Code,
            Date = reference.Date,
            CodeCustomer = reference.CodeCustomer,
            CodeDiagnoser = reference.CodeDiagnoser,
            Status = reference.Status
        };
        }

        public async Task<Reference> Convert(BLReference reference)
        {
        if (reference.Code != 0)
            return (await References.Read(re => re.Code == reference.Code)).FirstOrDefault();

        return new Reference
        {
            CodeDiagnoser = reference.CodeDiagnoser,
            CodeCustomer = reference.CodeCustomer,
            Status = reference.Status,
            Date = reference.Date
        };
        }

        public async Task Create(BLReference item)
        {
        if ((await Diagnosers.Read(d => d.Code == item.CodeDiagnoser)).Count == 0)
            throw new ReferenceException("מאבחנת");

        if ((await Customers.Read(c => c.Code == item.CodeCustomer)).Count == 0)
            throw new ReferenceException("לקוח");

        if ((await Statuses.Read()).Where(s => s.Code == item.Status).ToList().Count == 0)
            throw new ReferenceException("סטטוס");

        if (item.Date < DateTime.Today)
            throw new ReferenceException(item.Date);

        await References.Create(await Convert(item));
        }

        public async Task<BLCustomer> Customer(BLReference bLReference)
        {
        var reference = await Convert(bLReference);
        var customer = await References.Customer(reference);
        return Customers.Convert(customer);
        }

        public async Task Delete(BLReference item)
        {
        if (item == null) throw new SigninWorkShopExeption();
        if (item.Code == null) throw new SigninWorkShopExeption("קוד", false);

        await References.Delete(await Convert(item));
        }

        public async Task<BLDiagnoser> Diagnoser(BLReference bLReference)
        {
        var reference = await Convert(bLReference);
        var diagnoser = await References.Diagnoser(reference);
        return Diagnosers.Convert(diagnoser);
        }

        public async Task<List<BLReference>> Read()
        {
        var list = new List<BLReference>();
        var r = await References.Read();
        r.ForEach(x => list.Add(Convert(x)));
        return list;
        }

        public async Task<List<BLReference>> Read(Func<BLReference, bool> func)
        {
        return (await Read()).Where(func).ToList();
        }

        public async Task<BLStatus> Status(BLReference bLReference)
        {
        var reference = await Convert(bLReference);
        var status = await References.GetStatus(reference);
        return Statuses.Convert(status);
        }

        public async Task Update(BLReference item)
        {
        var r = await Convert(item);
        r.CodeCustomer = item.CodeCustomer;
        r.CodeDiagnoser = item.CodeDiagnoser;
        r.Status = item.Status;
        r.Date = item.Date;

        try
        {
            await References.Update(r);
        }
        catch
        {
            throw new Exception("לא קיימת הפניה כזו במערכת זו");
        }
        }
        }
        }
        */
    }
}
