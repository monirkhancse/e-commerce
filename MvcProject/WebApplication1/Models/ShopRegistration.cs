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

    public partial class ShopRegistration
    {
        public int ShopRegistrationID { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [DisplayName("Shop Name")]
        public string ShopName { get; set; }
        public string Proprietor { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        [DisplayName("Registration Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> RegistrationDate { get; set; }
    }
}
