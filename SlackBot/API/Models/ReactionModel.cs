using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
    public class ReactionModel
    {
        public string Type { get; set; }

        public string User { get; set; }

        public string Reaction { get; set; }

        public string Event_TS { get; set; }

        public ItemModel Item { get; set; }
    }

    public class ItemModel
    {
        public string Type { get; set; }

        public string Channel { get; set; }

        public string TS { get; set; }
    }
}
