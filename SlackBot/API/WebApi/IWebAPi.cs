using API.Models;
using System;

namespace API.WebApi
{
    public interface IWebApi
    {
        SavePostResponseModel SaveQuestion(Message message);

        SavePostResponseModel SaveAnswer(Message message);

        SavePostResponseModel SaveVote(ReactionModel reaction);
    }
}
