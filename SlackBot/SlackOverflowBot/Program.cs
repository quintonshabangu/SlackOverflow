using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using MargieBot;
using MargieBot.Responders;
using SlackOverflowBot.DI;
using Newtonsoft.Json.Linq;
using SlackOverflowBot.Models;
using Newtonsoft.Json;
using SlackOverflowBot.Responders;

namespace SlackOverflowBot
{
    public class Program
    {
        private static IWindsorContainer container;

        static void Main(string[] args)
        {
            container = BotRunnerBootstrapper.Init();

            var bot = new Bot();

            var responders = container.ResolveAll<IResponder>();
            foreach (var responder in responders)
            {
                bot.Responders.Add(responder);
            }

            //Run the reaction Responder (Custom made because Margiebot responders only handle messages)
            ReactionResponder.Instance.HandleVotes(bot);

            var connect = bot.Connect(ConfigurationManager.AppSettings["SlackBotApiToken"]);

            while (Console.ReadLine() != "close")
            {

            }
        }
    }
}
