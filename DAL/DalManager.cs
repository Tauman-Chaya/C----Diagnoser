using DAL.Api;
using DAL.Models;
using DAL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalManager
    {
        public IDiagnoser Diagnoser { get; }
        public ICustomer Customer { get; }
        public IWorkshop Workshop { get; }
        public ITypeGroup TypeGroup { get; }
        public IReference Reference { get; }
        public  IStatus Status { get; }
        public ILead Lead { get; }

        public DalManager()
        {
            ServiceCollection services = new();

            services.AddSingleton<DbManager>();
            services.AddSingleton<IDiagnoser, DiagnoserServices>();
            services.AddSingleton<ICustomer, CustomerService>();
            services.AddSingleton<IWorkshop, WorkshopServices>();
            services.AddSingleton<ITypeGroup, TypeGroupServices>();
            services.AddSingleton<IReference, ReferenceServices>();
            services.AddSingleton<IStatus, StatusServices>();
            services.AddSingleton<ILead, LeadServices>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            Diagnoser = serviceProvider.GetRequiredService<IDiagnoser>();
            Customer = serviceProvider.GetRequiredService<ICustomer>();
            Workshop = serviceProvider.GetRequiredService<IWorkshop>();
            TypeGroup = serviceProvider.GetRequiredService<ITypeGroup>();
            Reference = serviceProvider.GetRequiredService<IReference>();
            Status = serviceProvider.GetRequiredService<IStatus>();
            Lead = serviceProvider.GetRequiredService<ILead>();

        }
    }
}
