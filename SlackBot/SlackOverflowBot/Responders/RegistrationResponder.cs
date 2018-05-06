using MargieBot.Responders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MargieBot.Models;
using API.WebApi;
using System.Configuration;

namespace SlackOverflowBot.Responders
{
    public class RegistrationResponder : IResponder
    {
        IWebApi webApi;

        public RegistrationResponder()
        {
            this.webApi = new WebApi(ConfigurationManager.AppSettings["ServerAddress"],
                        ConfigurationManager.AppSettings["SlackBotApiToken"],
                        ConfigurationManager.AppSettings["ChannelId"]);
        }

        public bool CanRespond(ResponseContext context)
        {
            return !context.BotHasResponded &&
                    (context.Message.MentionsBot ||
                     context.Message.ChatHub.Type == SlackChatHubType.DM) &&
                    (context.Message.Text.ToLower().Contains("register") ||
                     context.Message.Text.ToLower().Contains("registration"));
        }

        public BotMessage GetResponse(ResponseContext context)
        {
            webApi.RegisterUser(context.Message.User.ID);
            return new BotMessage { Text = "You have been registered!" };
        }
    }
}
