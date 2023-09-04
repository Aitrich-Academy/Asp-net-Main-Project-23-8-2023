//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            this.ORDERS = new HashSet<ORDER>();
        }
    
        public int PRO_ID { get; set; }
        public string PRO_NAME { get; set; }
        public int PROCAT_ID { get; set; }
        public string PRO_DESC { get; set; }
        public int PRO_STOCK { get; set; }
        public byte[] PRO_IMAGE { get; set; }
        public int PRO_PRICE { get; set; }
        public string PRO_STATUS { get; set; }
        public string PRO_CREATEBY { get; set; }
        public string PRO_CREATEDATE { get; set; }
        public string PRO_MODIBY { get; set; }
        public string PRO_MODIDATE { get; set; }
    
        public virtual CATEGORY CATEGORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER> ORDERS { get; set; }
    }
}