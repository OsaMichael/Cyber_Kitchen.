using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Infrastructure
{
    public class DataAccess : NinjectModule
    {
        public override void Load()
        {
            
            Kernel.Bind<DbContext>().ToSelf().InRequestScope();
            Bind<ApplicationDbContext>().To<ApplicationDbContext>().InRequestScope();
            Bind<IDataRepository>().To<EntityRepository>().InRequestScope();
           
        }
    }
}