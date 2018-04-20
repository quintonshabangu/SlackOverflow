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

        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateReaction(ReactionDTO reactionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model state not valid. Check parameters");

            Post post = _context.Posts.SingleOrDefault(p => p.TimeStamp.Equals(reactionDTO.MessageTimeStamp));

            if (post != null && reactionDTO.SlackUserId != post.SlackUserId)
            {
                if (reactionDTO.Reaction.Equals("+1"))
                    post.AddUpVotesToPost(1);
                if (reactionDTO.Reaction.Equals("-1"))
                    post.AddDownVotesToPost(1);

                addPointsToUsers(post, reactionDTO);
                _context.SaveChanges();
            }

            return Ok();
        }

        private void addPointsToUsers(Post post, ReactionDTO reactionDTO)
        {
            Points points;

            if (reactionDTO.Reaction.Equals("+1"))
                points = RefereeHelper.Instance.VoterVotedAnswerUp();
            else if (reactionDTO.Reaction.Equals("-1"))
                points = RefereeHelper.Instance.VoterVotedAnswerDown();
            else
                return;

            var voter = _context.Users.SingleOrDefault(v => v.SlackId.Equals(reactionDTO.SlackUserId));

            if (voter != null)
                voter.AddUserPoints(points.VoterPoints);

            //Adding points to the Original Poster
            if (post != null && post.ApplicationUser != null)
                post.ApplicationUser.AddUserPoints(points.PosterPoints);

        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult RemoveReaction(ReactionDTO reactionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model state not valid. Check parameters");

            Post post = _context.Posts.SingleOrDefault(p => p.TimeStamp.Equals(reactionDTO.MessageTimeStamp));

            if (post != null && reactionDTO.SlackUserId != post.SlackUserId)
            {
                if (reactionDTO.Reaction.Equals("+1"))
                    post.AddUpVotesToPost(-1);
                if (reactionDTO.Reaction.Equals("-1"))
                    post.AddDownVotesToPost(-1);

                undoAddPointsToUsers(post, reactionDTO);
                _context.SaveChanges();
            }

            return Ok();
        }

        private void undoAddPointsToUsers(Post post, ReactionDTO reactionDTO)
        {
            Points points;

            if (reactionDTO.Reaction.Equals("+1"))
                points = RefereeHelper.Instance.VoterRemovedUpVote();
            else if (reactionDTO.Reaction.Equals("-1"))
                points = RefereeHelper.Instance.VoterRemovedDownVote();
            else
                return;

            var voter = _context.Users.SingleOrDefault(v => v.SlackId.Equals(reactionDTO.SlackUserId));

            if (voter != null)
                voter.AddUserPoints(points.VoterPoints);

            //Adding points to the Original Poster
            if (post != null && post.ApplicationUser != null)
                post.ApplicationUser.AddUserPoints(points.PosterPoints);

        }

    }
}
