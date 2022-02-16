using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SalesController : Controller
    {
        private MvcProjectDBEntities1 db = new MvcProjectDBEntities1();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Customer).Include(s => s.Store).Include(s => s.Product);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "store_name");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "stock_status");
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "stock_status");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleId,ProductId,CustomerId,StoreId,sale_date,rate,quantity,total_price,vat,discount,net_total_price,stock_status,memo_no,coomments")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);

                // update stock
                var oStock = (from o in db.Stocks where o.ProductId == sale.ProductId select o).FirstOrDefault();
                if (oStock != null)
                {
                    oStock.Quantity -= sale.quantity;
                    oStock.Stutus = "out";
                }
                // end of update stock

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", sale.CustomerId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "store_name", sale.StoreId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", sale.ProductId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProducName", sale.ProductId);
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "stock_status", sale.SaleId);
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "stock_status", sale.SaleId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", sale.CustomerId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "store_name", sale.StoreId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", sale.ProductId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProducName", sale.ProductId);
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "stock_status", sale.SaleId);
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "stock_status", sale.SaleId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleId,ProductId,CustomerId,StoreId,sale_date,rate,quantity,total_price,vat,discount,net_total_price,stock_status,memo_no,coomments")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", sale.CustomerId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "store_name", sale.StoreId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", sale.ProductId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProducName", sale.ProductId);
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "stock_status", sale.SaleId);
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "stock_status", sale.SaleId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);

            // update stock
            var oStock = (from o in db.Stocks where o.ProductId == sale.ProductId select o).FirstOrDefault();
            if (oStock != null)
            {
                oStock.Quantity += sale.quantity;
                oStock.Stutus = "in";
            }
            // end of update stock

            db.Sales.Remove(sale);
            db.SaveChanges();
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
    }
}