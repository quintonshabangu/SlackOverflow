using MargieBot.Responders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MargieBot.Models;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using API.WebApi;
using API.ChatApi;
using API.Models;
using System.Net;

namespace SlackOverflowBot.Responders
{
    public class QuestionResponder : IResponder
    {
        private readonly IWebAPi webApi;
        private readonly IChatApi chatApi;

        public QuestionResponder(IChatApi chatApi)
        {
            webApi = new WebApi();
            this.chatApi = chatApi;
        }

        public bool CanRespond(ResponseContext context)
        {
            return !context.BotHasResponded &&
                    context.Message.Text.Contains("Question:") &&
                    context.Message.ChatHub.ID.Equals(ConfigurationManager.AppSettings["ChannelId"]);
        }

        public BotMessage GetResponse(ResponseContext context)
        {
            var builder = new StringBuilder();
            builder.Append("Can someone please help ");
            builder.Append(context.Message.User.FormattedUserID);
            builder.Append("? Thanks :)");
            if (SaveQuestion(context.Message))
            {
                chatApi.PostMessage(ConfigurationManager.AppSettings["SlackBotApiToken"], context.Message.User.ID, "Your Question has been posted.");
                return new BotMessage();
            }

            //If there are errors
            chatApi.PostMessage(ConfigurationManager.AppSettings["SlackBotApiToken"], context.Message.User.ID, "There was an error posting your question. Please contact the Administrator");
            return new BotMessage();
        }

        private bool SaveQuestion(SlackMessage slackMessage)
        {
            Message message = new Message
            {
                text = slackMessage.Text,

                StringTimeStamp = JsonConvert.DeserializeObject<JObject>(slackMessage.RawData)
                .SelectToken("ts")
                .ToString(),

                user = slackMessage.User.ID
            };

            var response = webApi.SaveQuestion(message);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
