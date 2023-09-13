using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_COMMERCE.Models
{
    public class Ent_Orders
    {
       
            public int? orderId { get; set; }
            public int Ord_userId { get; set; }
            public int Ord_proId { get; set; }
            public int Ord_qty { get; set; }
            public int Ord_total { get; set; }
            public string Ord_status { get; set; }
            public string Ord_createBy { get; set; }
            public string Ord_createDate { get; set; }
            public string Ord_modiBy { get; set; }
            public string Ord_modiDate { get; set; }
            public virtual PRODUCT PRODUCT { get; set; }
            public virtual USER USER { get; set; }
      
    }
}