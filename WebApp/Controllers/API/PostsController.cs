using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApp.Controllers.Helpers;
using WebApp.Models;
using WebApp.Models.Domain;
using WebApp.Models.Domain.ReferenceModels;
using WebApp.Models.DTOs;

namespace WebApp.Controllers.API
{
    public class PostsController : ApiController
    {
        ApplicationDbContext _context;

        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult SavePost(PostDTO postDTO)
        {
            if (ModelState.IsValid)
            {
                Post newPost = Mapper.Map<PostDTO, Post>(postDTO);

                ApplicationUser poster = _context.Users
                    .SingleOrDefault(u => u.SlackId == postDTO.SlackUserId);

                if (SavePostType(newPost, postDTO))
                    SaveUserPoints(newPost, poster);

                newPost.ApplicationUser = poster;
                return SavePost(newPost);
            }

            return BadRequest();
        }

        private bool SavePostType(Post newPost, PostDTO postDTO)
        {
            var postType = _context.PostTypes.SingleOrDefault(pt => pt.Id == postDTO.PostTypeId);

            if (postType == null)
                return false;

            newPost.PostType = postType;
            return true;
        }

        private void SaveUserPoints(Post newPost, ApplicationUser user)
        {
            if (user == null)
                return; //There is nothing wrong. It means the person who posted is not registered

            Points points;
            switch (newPost.PostType.Id)
            {
                case PostType.Question:
                     points = RefereeHelper.Instance.QuestionerPostsQuestionPoints();
                    user.AddUserPoints(points.PosterPoints);
                    break;

                case PostType.Answer:
                    points = RefereeHelper.Instance.AnswererPostsAnswerPoints();
                    user.AddUserPoints(points.PosterPoints);
                    break;

                default:
                    return;
            }
        }

        private IHttpActionResult SavePost(Post newPost)
        {
            try
            {
                _context.Posts.Add(newPost);
                _context.SaveChanges();
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

    }
}
