using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.DI;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace YodaSlackBot.DI
{
    public static class BotRunnerBootstrapper
    {
        public static IWindsorContainer Init()
        {
            var container = Bootstrapper.Init();
            
            container.Install(FromAssembly.This());

            return container;
        }
    }
}
