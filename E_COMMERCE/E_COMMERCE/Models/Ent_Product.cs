using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_COMMERCE.Models
{
    public class Ent_Product
    {
        public int? productid { get; set; }

        public string productname { get; set; }

        public int procatid { get; set; }
       
        public string productdescription { get; set; }

        public int productstock { get; set; }

        public Byte[] productimage { get; set; }

        public int productprice { get; set; }

        public string productstatus { get; set; }

        public string productcreatedby { get; set; }
      
        public string productcreateddate { get; set; }

        public string productmodifiedby { get; set; }

        public string productmodifieddate { get; set; }


    }
}