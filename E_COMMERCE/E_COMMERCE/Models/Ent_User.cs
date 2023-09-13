using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECOMMERSE.Models
{
    public class Ent_User
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public Byte[] image { get; set; } = null;

        public string role { get; set; } = null;
        public string status { get; set; } = null;
        public string createBy { get; set; }
        public string createdate { get; set; } = null;
        public string modiBy { get; set; }
        public string modiDate { get; set; } = null;
    }
}