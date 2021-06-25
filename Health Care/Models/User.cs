using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class User
    {
        public int id { get; set; }
        public string nameAR { get; set; }
        public string nameEN { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public int regionId { get; set; }
        public string email { get; set; } // to make all user activation account using it
        public string Password { get; set; }
        public string DeviceId { get; set; }
        public bool isActiveAccount { get; set; }
        public int Roleid { get; set; }
        public bool completeData { get; set; }
        public bool active { get; set; }
        //public ICollection<Role> Roles { get; set; }
        public virtual RefreshToken RefreshTokens { get; set; }

    }
}
