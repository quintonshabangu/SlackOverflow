using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using RestSharp;
using API.GroupsApi;
using System.Configuration;

namespace API.WebApi
{
    public class WebApi : IWebApi
    {
        private string ServerAddress;

        public WebApi(string ServerAddress)
        {
            this.ServerAddress = ServerAddress;
        }

        public SavePostResponseModel SaveQuestion(Message message)
        {
            var client = new RestClient(ServerAddress);
            var request = new RestRequest("Posts", Method.POST);

            request.AddParameter("Text", message.text);
            request.AddParameter("slackUserId", message.user);
            request.AddParameter("TimeStamp", message.StringTimeStamp);
            request.AddParameter("IsQuestion", true );

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
            var client = new RestClient(ServerAddress);
            var request = new RestRequest("Reactions", Method.POST);

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
    }
}
