﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackOverflowBot.Models
{
    public class SlackRawMessageModel
    {
        public string type { get; set; }
        public string channel { get; set; }
        public string user { get; set; }
        public string text { get; set; }
        public string ts { get; set; }
        public string team { get; set; }
    }
}
