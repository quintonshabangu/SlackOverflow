using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.DTOs
{
    public class ReactionDTO
    {
        [Required]
        public string SlackUserId { get; set; }

        [Required]
        public string Reaction { get; set; }

        [Required]
        public string MessageTimeStamp { get; set; }
    }

}