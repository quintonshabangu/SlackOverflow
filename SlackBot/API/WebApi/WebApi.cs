using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using RestSharp;
using API.GroupsApi;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using API.UserApi;
using System.Net;

namespace API.WebApi
{
    public class WebApi : IWebApi
    {
        private string ServerAddress;
        private string SlackApiToken;
        private string SlackChannel;

        public WebApi(string ServerAddress, string SlackBotApiToken, string SlackChannel)
        {
            this.ServerAddress = ServerAddress;
            this.SlackApiToken = SlackBotApiToken;
            this.SlackChannel = SlackChannel;
        }

        public SavePostResponseModel SaveQuestion(Message message)
        {
            var client = new RestClient(ServerAddress);
            var request = new RestRequest("Posts", Method.POST);

            request.AddParameter("Text", message.text);
            request.AddParameter("slackUserId", message.user);
            request.AddParameter("TimeStamp", message.StringTimeStamp);
            request.AddParameter("PostTypeId", (int)PostType.Question);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return new SavePostResponseModel { StatusCode = response.StatusCode };

            //If there are errors
            return new SavePostResponseModel
            {
                StatusCode = response.StatusCode,
                ErrorMessage = response.Content
            };
        }

        public SavePostResponseModel SaveAnswer(Message message)
        {
            var client = new RestClient(ServerAddress);
            var request = new RestRequest("Posts", Method.POST);

            request.AddParameter("Text", message.text);
            request.AddParameter("slackUserId", message.user);
            request.AddParameter("TimeStamp", message.StringTimeStamp);
            request.AddParameter("PostTypeId", (int)PostType.Answer);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return new SavePostResponseModel { StatusCode = response.StatusCode };

            //If there are errors
            return new SavePostResponseModel
            {
                StatusCode = response.StatusCode,
                ErrorMessage = response.Content
            };
        }

        public SavePostResponseModel SaveVote(ReactionModel reaction)
        {
            if (hasNotVoted(reaction)
                && (reaction.Reaction.Contains("+1") || reaction.Reaction.Contains("-1")))
            {
                var client = new RestClient(ServerAddress);
                var request = new RestRequest("Reactions/CreateReaction", Method.POST);

                request.AddParameter("SlackUserId", reaction.User);
                request.AddParameter("Reaction", reaction.Reaction);
                request.AddParameter("MessageTimeStamp", reaction.Item.TS.ToString());

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return new SavePostResponseModel { StatusCode = response.StatusCode };

                //If there are errors
                return new SavePostResponseModel
                {
                    StatusCode = response.StatusCode,
                    ErrorMessage = response.Content
                };
            }

            return new SavePostResponseModel
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                ErrorMessage = "Your vote was not counted as you have voted before",
            };
        }

        private bool hasNotVoted(ReactionModel reaction)
        {
            List<Reaction> reactions = getReactions(reaction.Item.TS);

            //Checks if the voter has voted for the same post before
            int userCount = reactions.Where(r => r.users.Contains(reaction.User)).Count();

            if (userCount > 1)
                return false;

            return true;
        }

        private List<Reaction> getReactions(string timeStamp)
        {
            var client = new RestClient("https://slack.com/api");
            var request = new RestRequest("reactions.get", Method.GET);

            request.AddQueryParameter("token", SlackApiToken);
            request.AddQueryParameter("channel", SlackChannel);
            request.AddQueryParameter("timestamp", timeStamp);

            var rawResponse = client.Execute(request).Content;

            if (!rawResponse.Contains("{\"ok\":false,\"error\":\"message_not_found\"}"))
            {
                var response = JsonConvert.DeserializeObject<JObject>(rawResponse).SelectToken("message.reactions");
                var reactions = JsonConvert.DeserializeObject<List<Reaction>>(response.ToString());
                return reactions;
            }

            return new List<Reaction>();
        }

        public SavePostResponseModel RemoveVote(ReactionModel reaction)
        {
            var client = new RestClient(ServerAddress);
            var request = new RestRequest("Reactions/RemoveReaction", Method.POST);

            request.AddParameter("SlackUserId", reaction.User);
            request.AddParameter("Reaction", reaction.Reaction);
            request.AddParameter("MessageTimeStamp", reaction.Item.TS);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return new SavePostResponseModel { StatusCode = response.StatusCode };

            //If there are errors
            return new SavePostResponseModel
            {
                StatusCode = response.StatusCode,
                ErrorMessage = response.Content
            };
        }

        public bool RegisterUser(string slackId)
        {
            var user = GetUser(slackId);

            var client = new RestClient(ServerAddress);
            var request = new RestRequest("api/Account/Register", Method.POST);

            request.AddParameter("Id", user.id);
            request.AddParameter("FirstName", user.profile.first_name);
            request.AddParameter("LastName", user.profile.last_name);
            request.AddParameter("Email", user.profile.email);
            request.AddParameter("SlackToken", SlackApiToken);

            var rawResponse = client.Execute(request);

            if (rawResponse.StatusCode == HttpStatusCode.OK)
                return true;

            return false;
        }

        private User GetUser(string slackId)
        {
            UserApi.UserApi userApi = new UserApi.UserApi();
            return userApi.GetUserInfo(SlackApiToken, slackId).user;
        }
    }
}
