﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class User
    {
        public int id { get; set; }
        public int regionId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public int Roleid { get; set; }
        public string email { get; set; } // to make all user activation account using it
        public string Password { get; set; }
        public bool isActiveAccount { get; set; }


    }
}
