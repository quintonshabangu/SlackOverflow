using Castle.Windsor;
using Castle.Windsor.Installer;

namespace API.DI
{
    public class Bootstrapper
    {
        public static IWindsorContainer Init()
        {
            return new WindsorContainer()
                .Install(FromAssembly.This()
                );
        }
    }
}
