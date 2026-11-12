using BL.API;
using BL.Services;
using DAL;
using DAL.Api;
using DAL.Models;
using DAL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLManager
    {
        public IBLDiagnoser diagnoser { get; }
        public IBLCustomer customer { get; }
        public IBLWorkshop workshop { get; }
        public IBLTypeGroup typeGroup { get; }
        public IBLReference reference { get; }
        public IBLStatus status { get; }
        public IBLLead lead { get; }

        public BLManager()
        {
            ServiceCollection services = new();

            services.AddSingleton<DalManager>();
            services.AddSingleton<DbManager>();
            services.AddSingleton<IBLDiagnoser, BLDiagnoserServices>();
            services.AddSingleton<IBLCustomer, BLCustomerServices>();
            services.AddSingleton<IBLWorkshop, BLWorkshopServices>();
            services.AddSingleton<IWorkshop, WorkshopServices>();
            services.AddSingleton<IBLTypeGroup, BLTypeGroupServices>();
            services.AddSingleton<IBLReference, BLReferenceServices>();
            services.AddSingleton<IBLStatus, BLStatusServices>();
            services.AddSingleton<IBLLead, BLLeadServices>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            diagnoser = serviceProvider.GetRequiredService<IBLDiagnoser>();
            customer = serviceProvider.GetRequiredService<IBLCustomer>();
            workshop = serviceProvider.GetRequiredService<IBLWorkshop>();
            typeGroup = serviceProvider.GetRequiredService<IBLTypeGroup>();
            reference = serviceProvider.GetRequiredService<IBLReference>();
            status = serviceProvider.GetRequiredService<IBLStatus>();
            lead = serviceProvider.GetRequiredService<IBLLead>();
        }
    }
}
