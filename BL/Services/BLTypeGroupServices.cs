using BL.API;
using BL.Exeptions;
using BL.Model;
using DAL;
using DAL.Models;
using DAL.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BLTypeGroupServices : IBLTypeGroup
    {
        ITypeGroup TypeGroup;

        public BLTypeGroupServices(DalManager dalManager)
        {
            this.TypeGroup = dalManager.TypeGroup;
        }

        public async Task<int> Code(string name)
        {
            try
            {
                List<BLTypeGroup> typeGroup = await Read(t => t.Description.Trim().CompareTo(name.Trim()) == 0);
                return typeGroup.FirstOrDefault().Code;
            }
            catch { return 0; }
        }

        public BLTypeGroup Convert(TypeGroup group)
        {
            return new()
            {
                Code = group.Code,
                Description = group.Description
            };
        }

        public async Task<TypeGroup> Convert(BLTypeGroup group)
        {
            TypeGroup t = (await TypeGroup.Read(t => t.Code == group.Code)).FirstOrDefault();
            if (t != null)
                return t;
            return new()
            {
                Code = group.Code,
                Description = group.Description
            };
        }

        public async Task Create(BLTypeGroup item)
        {
            if (item == null)
                throw new ArgumentNullException();
            if (item.Description == null)
                throw new SigninWorkShopExeption("תיאור הקבוצה", false);
            if (Code(item.Description).Result != 0)
                throw new Exception("קבוצה זו כבר קיימת במערכת");
            if (item.Code == null)
                throw new SigninWorkShopExeption("קוד הקבוצה", false);
            await TypeGroup.Create(await Convert(item));
        }

        public async Task Delete(BLTypeGroup item)
        {
            if (item == null)
                throw new ArgumentNullException();
            if (item.Description == null)
                throw new SigninWorkShopExeption("תיאור הקבוצה", false);
            if (item.Code == null)
                throw new SigninWorkShopExeption("קוד הקבוצה", false);
            if (Code(item.Description).Result != item.Code)
                throw new Exception("קבוצה זו לא קיימת במערכת");
            await TypeGroup.Delete(await Convert(item));
        }

        public async Task<string> Name(int code)
        {
            try
            {
                List<BLTypeGroup> typeGroups = await Read(t => t.Code == code);
                return typeGroups.FirstOrDefault().Description;
            }
            catch { return null; }
        }

        public async Task<List<BLTypeGroup>> Read()
        {
            List<BLTypeGroup> list = new();
            List<TypeGroup> typeGroups = await TypeGroup.Read();
            typeGroups.ForEach(t=>list.Add(Convert(t)));
            return list;
        }

        public async Task<List<BLTypeGroup>> Read(Func<BLTypeGroup, bool> func)
        {
            return (await Read()).Where(t=>func(t)).ToList();
        }

        public async Task Update(BLTypeGroup item)
        {
            if (item == null)
                throw new ArgumentNullException();
            if (item.Description == null)
                throw new SigninWorkShopExeption("תיאור הקבוצה", false);
            if ((await Code(item.Description)) != 0)
                throw new Exception("קבוצה זו כבר קיימת במערכת");
            if (item.Code == null)
                throw new SigninWorkShopExeption("קוד הקבוצה", false);
            if ((await Name(item.Code)) == null)
                throw new Exception("לא קיימת קבוצה מערכת עם קוד זהה");
            TypeGroup t = (await Convert(item));
            t.Description = item.Description;
            await TypeGroup.Update(t);
        }
    }
}
