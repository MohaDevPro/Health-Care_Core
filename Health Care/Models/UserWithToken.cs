using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mr.Delivery.Models
{
    public class UserWithToken
    {
        public int id { get; set; }
        public string nameAR { get; set; }
        public string nameEN { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public int SpecificId { get; set; }
        public int regionId { get; set; }
        public int roleid { get; set; }
        public string email { get; set; } 
        public string type { get; set; } 
        public bool isActiveAccount { get; set; } 
        public string DeviceId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
