using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_COMMERCE.Models
{
    public class Ent_Category
    {
        public int? Catid { get; set; }
        public string Catname { get; set; }
        public string Catdesc { get; set;}
        public Byte[] Catimage { get; set; } = null;
        public string Catstatus { get; set; }
        public string Catcreatedby { get; set; }
        public string Catcreateddate { get;set; }
        public string Catmodifiedby { get; set; }
        public string Catmodifieddate { get; set; }


    }
}