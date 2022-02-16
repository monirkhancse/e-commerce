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
    using System.ComponentModel.DataAnnotations;

    public partial class Sale
    {
        public int SaleId { get; set; }
        [DisplayName("Product Id")]
        public Nullable<int> ProductId { get; set; }
        [DisplayName("Customer Id")]
        public Nullable<int> CustomerId { get; set; }
        [DisplayName("Store Id")]
        public Nullable<int> StoreId { get; set; }
        [DisplayName("Sales Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> sale_date { get; set; }
        [DisplayName("Rate")]
        public Nullable<decimal> rate { get; set; }
        [DisplayName("Quantity")]
        public Nullable<int> quantity { get; set; }
        [DisplayName("Total Price")]
        public Nullable<decimal> total_price { get; set; }
        [DisplayName("Vat")]
        public Nullable<decimal> vat { get; set; }
        [DisplayName("Discount")]
        public Nullable<decimal> discount { get; set; }
        [DisplayName("Net Total Price")]
        public Nullable<decimal> net_total_price { get; set; }
        [DisplayName("Stock Status")]
        public string stock_status { get; set; }
        [DisplayName("Memo No.")]
        public Nullable<int> memo_no { get; set; }
        [DisplayName("Comments")]
        public string comments { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
