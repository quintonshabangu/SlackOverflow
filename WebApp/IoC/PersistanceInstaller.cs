using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApp.Models;

namespace WebApp.IoC
{
    public class PersistenceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<EntityFrameworkFacility>();
        }
    }

    public class EntityFrameworkFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.Register(Component.For<IDbContext>()
                                     .ImplementedBy<MyEntities>()
                                     .LifestylePerWebRequest(),
                            Component.For(typeof(IRepository<>))
                                     .ImplementedBy(typeof(Repository<>))
                                     .LifestylePerWebRequest());
        }
    }
}