﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class Email
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
    }
}
