using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
        private MvcProjectDBEntities1 db = new MvcProjectDBEntities1();

        // GET: Products
        public ActionResult Index(string search,string searchBy)
        {

            //if (searchBy == "email")
            //{
            //    return View(db.Customers.Where(x => x.Email == search || search == null).ToList());
            //}
            //else
            //{
            //    return View(db.Customers.Where(x => x.CustomerName.StartsWith(search) || search == null).ToList());

            //}
            //var products = db.Products.Include(p => p.Category);
            var products = from p in db.Products
                           join s in db.Stocks on p.ProductId equals s.ProductId
                           select new VmProduct
                           {
                               ProductId = p.ProductId,
                               buying_price = p.buying_price,
                               Image = p.Image,
                               ProductName = p.ProductName,
                               CategoryId = p.CategoryId,
                               Category = p.Category,
                               selling_price = p.selling_price,
                               quantity = s.Quantity
                           };
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,buying_price,selling_price,CategoryId,Image,File")] Product product)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(product.File.FileName);
                string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                product.Image = "~/Images/" + _filename;
                db.Products.Add(product);
                if (product.File.ContentLength < 1000000)
                {
                    if (db.SaveChanges() > 0)
                    {
                        product.File.SaveAs(path);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.msg = "File must less then or Equal to 1 MB";
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            Session["imgPath"] = product.Image;//
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,buying_price,selling_price,CategoryId,Image,File")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.File != null)
                {
                    string filename = Path.GetFileName(product.File.FileName);
                    string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                    string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                    product.Image = "~/Images/" + _filename;

                    if (product.File.ContentLength < 1000000)
                    {
                        db.Entry(product).State = EntityState.Modified;
                        string oldImgPath = Request.MapPath(Session["imgPath"].ToString());
                        if (db.SaveChanges() > 0)
                        {

                            product.File.SaveAs(path);
                            if (System.IO.File.Exists(oldImgPath))
                            {
                                System.IO.File.Delete(oldImgPath);
                            }
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.msg = "File must less than or equal to 1 MB";
                    }
                }
                else
                {
                    product.Image = Session["imgPath"].ToString();
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);

            string currentImg = Request.MapPath(product.Image);
            db.Products.Remove(product);
            db.SaveChanges(); //
            if (db.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(currentImg))
                {
                    System.IO.File.Delete(currentImg);
                }
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: products/GetStockByProductId/5
        public JsonResult GetStockByProductId(int? id)
        {
            if (id == null)
            {
                return Json(new { message = "invalid product id" }, JsonRequestBehavior.AllowGet);
            }
            var oStock = db.Stocks.Find(id);
            var oStockOfProduct = (from p in db.Products
                                   join s in db.Stocks on p.ProductId equals s.ProductId
                                   where p.ProductId == id
                                   select new
                                   {
                                       p.ProductId,
                                       p.selling_price,
                                       s.Quantity
                                   }).FirstOrDefault();
            if (oStockOfProduct == null)
            {
                return Json(new { message = "product not found" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { pid = oStockOfProduct.ProductId, qty = oStockOfProduct.Quantity, salePrice = oStockOfProduct.selling_price }, JsonRequestBehavior.AllowGet);
        }
    }
}