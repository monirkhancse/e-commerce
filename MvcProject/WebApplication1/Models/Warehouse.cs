//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Warehouse
    {
        public int warehouseID { get; set; }
        [DisplayName("Warehouse No.")]
        public Nullable<int> warehouseNo { get; set; }
        [DisplayName("Warehouse Name")]
        public string warehouseName { get; set; }
        [DisplayName("Manager Name")]
        public string warehouseManager { get; set; }
        [DisplayName("Address")]
        public string address { get; set; }
        public Nullable<int> PurchaseId { get; set; }
    
        public virtual Purchase Purchase { get; set; }
    }
}