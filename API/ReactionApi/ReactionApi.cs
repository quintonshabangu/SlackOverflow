﻿using System.Configuration;
using Flurl.Http;
using ServiceStack.Text;

namespace API.ReactionApi
{
    public class ReactionApi : IReactionApi
    {
        public bool AddReaction(string reaction, string channel, string timestamp)
        {
            var responseString = "https://slack.com/api/reactions.add"
                .PostUrlEncodedAsync(
                    new
                    {
                        token = ConfigurationManager.AppSettings["SlackBotApiToken"],
                        name = reaction,
                        channel,
                        timestamp
                    })
                .ReceiveString()
                .Result;

            var serializer = new JsonSerializer<ReactionResponseModel>();
            var response = serializer.DeserializeFromString(responseString);
            return response.ok;
        }

        public int GetReactionsForAnswer(string timeStamp)
        {
            var responseString = "https://slack.com/api/reactions.get"
                .PostUrlEncodedAsync(
                    new
                    {
                        token = ConfigurationManager.AppSettings["SlackBotApiToken"],
                        channel = ConfigurationManager.AppSettings["ChannelId"],
                        timestamp = timeStamp
                    }
                )
                .ReceiveString()
                .Result;
            return 1;
        }
    }
}
