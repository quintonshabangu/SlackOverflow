using API.Models;
using System;

namespace API.WebApi
{
    public interface IWebAPi
    {
        SavePostResponseModel SaveQuestion(Message message);

        SavePostResponseModel SaveAnswer(Message message);
    }
}
