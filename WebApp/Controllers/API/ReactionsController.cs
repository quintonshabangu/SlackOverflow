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
using WebApp.Models.DTOs;

namespace WebApp.Controllers.API
{
    public class ReactionsController : ApiController
    {
        ApplicationDbContext _context;

        public ReactionsController()
        {
            _context = new ApplicationDbContext();
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult CreateReaction(ReactionDTO reactionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model state not valid. Check parameters");

            Post post = _context.Posts.SingleOrDefault(p => p.TimeStamp.Equals(reactionDTO.MessageTimeStamp));

            if (post != null)
                post.Votes = post.Votes + 1;

            addPointsToUsers(post, reactionDTO.SlackUserId);

            _context.SaveChanges();

            return Ok();
        }

        private void addPointsToUsers(Post post, string voterSlackId)
        {
            Points points = RefereeHelper.Instance.VoterVotedAnswerUp();

            var voter = _context.Users.SingleOrDefault(v => v.SlackId.Equals(voterSlackId));

            if (voter != null)
                voter.AddUserPoints(points.VoterPoints);

            //Adding points to the Original Poster
            if (post != null || post.ApplicationUser != null)
                post.ApplicationUser.AddUserPoints(points.PosterPoints);

        }
    }
}
