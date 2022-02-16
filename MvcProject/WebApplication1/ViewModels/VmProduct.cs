using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class VmProduct:Product
    {
        //public int ProductId { get; set; }
        //public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime? ExpireDate { get; set; }
        //public int CategoryId { get; set; }
        //public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
        public Nullable<int> quantity { get; set; }
        public List<Category> CategoryList { get; set; }

    }

}