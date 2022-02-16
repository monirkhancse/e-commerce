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
    public class PurchasesController : Controller
    {
        private MvcProjectDBEntities1 db = new MvcProjectDBEntities1();

        // GET: purchases
        public ActionResult Index()
        {
            var purchases = db.Purchases.Include(p => p.Product).Include(p => p.Store).Include(p => p.Supplier);
            return View(purchases.ToList());
        }

        // GET: purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: purchases/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "productId", "ProductName");
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "store_name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierName");
            return View();
        }

        // POST: purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseId,ProductId,SupplierId,StoreId,purchase_date,unit_price,quantity,total_price,vat,grand_total_price,stock_status,memo_no,coomments")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {

                Purchase opurchase = new Purchase();
                // insert into purchase
                //opurchase.unit_price = purchase.unit_price;
                //opurchase.grand_total_price = purchase.unit_price * purchase.quantity+purchase.vat;
                db.Purchases.Add(purchase);

                // update stock
                var oStock = (from o in db.Stocks where o.ProductId == purchase.ProductId select o).FirstOrDefault();
                if (oStock == null)
                {
                    oStock = new Stock();
                    oStock.ProductId = purchase.ProductId;
                    oStock.Quantity = purchase.quantity;
                    oStock.StoreId = purchase.StoreId;
                    oStock.Stutus = "in";
                    db.Stocks.Add(oStock);
                }
                else
                {
                    oStock.Quantity += purchase.quantity;
                    oStock.Stutus = "in";
                }
                // end of update stock

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", purchase.ProductId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "store_name", purchase.StoreId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "StoreId", "SupplierName", purchase.SupplierId);
            return View(purchase);

        }

        // GET: purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", purchase.ProductId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "store_name", purchase.StoreId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "StoreId", "SupplierName", purchase.StoreId);
            return View(purchase);
        }

        // POST: purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseId,ProductId,SupplierId,StoreId,purchase_date,unit_price,quantity,total_price,vat,grand_total_price,stock_status,memo_no,coomments")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", purchase.ProductId);
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "store_name", purchase.StoreId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierName", purchase.SupplierId);
            return View(purchase);
        }

        // GET: purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            // update stock
            var oStock = (from o in db.Stocks where o.ProductId == purchase.ProductId select o).FirstOrDefault();
            if (oStock != null)
            {
                oStock.Quantity -= purchase.quantity;
                oStock.Stutus = "out";
            }
            // end of update stock
            db.Purchases.Remove(purchase);
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
