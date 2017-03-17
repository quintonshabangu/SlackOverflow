using API.Models;
using API.WebApi;
using MargieBot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackOverflowBot.Responders
{
    public class ReactionResponder
    {
        private static ReactionResponder _Instace;
        private IWebApi webApi;

        public static ReactionResponder Instance
        {
            get
            {
                if (_Instace == null)
                {
                    _Instace = new ReactionResponder();
                }
                return _Instace;
            }
        }

        private ReactionResponder()
        {
            webApi = new WebApi(ConfigurationManager.AppSettings["SlackOverflowWeb"]);
        }

        public void HandleVotes(Bot bot)
        {
            //Message event handler
            bot.MessageReceived += (message) =>
            {
                ReactionModel reaction = JsonConvert.DeserializeObject<ReactionModel>(message);
                if (reaction.Type.Equals("reaction_added"))
                {
                    webApi.SaveVote(reaction);
                }
            };
        }
    }
}
