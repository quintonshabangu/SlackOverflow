using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Models.Domain;
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
        public IHttpActionResult CreatePost(PostDTO postDTO)
        {
            if (ModelState.IsValid)
            {
                Post newPost = Mapper.Map<PostDTO, Post>(postDTO);
                ApplicationUser applicationUser = _context.Users.SingleOrDefault(u => u.SlackId == postDTO.SlackUserId);

                //Sometimes users will not be registered on the app but do exist on slack
                if (applicationUser != null)
                {
                    newPost.ApplicationUser = applicationUser;
                    applicationUser.Points = applicationUser.Points + 5;

                    if (!postDTO.IsQuestion)
                        applicationUser.Points = applicationUser.Points +  5;
                }

                try
                {
                    _context.Posts.Add(newPost);
                    _context.SaveChanges();
                }
                    catch (Exception e)
                {
                    return InternalServerError();
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}
