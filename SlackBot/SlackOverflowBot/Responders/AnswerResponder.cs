﻿using MargieBot.Responders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MargieBot.Models;
using API.ChatApi;
using API.WebApi;
using System.Configuration;
using API.Models;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace SlackOverflowBot.Responders
{
    public class AnswerResponder : IResponder
    {
        private readonly IWebApi webApi;
        private readonly IChatApi chatApi;

        public AnswerResponder(IChatApi chatApi)
        {
            this.webApi = new WebApi(ConfigurationManager.AppSettings["ServerAddress"],
                                    ConfigurationManager.AppSettings["SlackBotApiToken"],
                                    ConfigurationManager.AppSettings["ChannelId"]);
            this.chatApi = chatApi;
        }

        public bool CanRespond(ResponseContext context)
        {
            return !context.BotHasResponded &&
                    context.Message.Text.Contains("a:") &&
                    context.Message.ChatHub.ID.Equals(ConfigurationManager.AppSettings["ChannelId"]);
        }

        public BotMessage GetResponse(ResponseContext context)
        {

            if (SaveAnswer(context.Message))
            {
                chatApi.PostMessage(ConfigurationManager.AppSettings["SlackBotApiToken"], context.Message.User.ID, "Your reply has been posted. Thank you for the input");
                return new BotMessage ();
            }
            chatApi.PostMessage(ConfigurationManager.AppSettings["SlackBotApiToken"], context.Message.User.ID, "There was an error posting your question.");
            return new BotMessage ();
        }

        private bool SaveAnswer(SlackMessage slackMessage)
        {
            Message message = new Message
            {
                text = slackMessage.Text,

                StringTimeStamp = JsonConvert.DeserializeObject<JObject>(slackMessage.RawData)
                .SelectToken("ts")
                .ToString(),

                user = slackMessage.User.ID
            };

            var response = webApi.SaveAnswer(message);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
