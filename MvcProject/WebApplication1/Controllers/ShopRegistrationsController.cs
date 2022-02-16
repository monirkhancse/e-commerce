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
    public class ShopRegistrationsController : Controller
    {
        private MvcProjectDBEntities1 db = new MvcProjectDBEntities1();

        // GET: ShopRegistrations
        public ActionResult Index()
        {
            return View(db.ShopRegistrations.ToList());
        }

        // GET: ShopRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopRegistration shopRegistration = db.ShopRegistrations.Find(id);
            if (shopRegistration == null)
            {
                return HttpNotFound();
            }
            return View(shopRegistration);
        }

        // GET: ShopRegistrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShopRegistrationID,UserName,Password,ShopName,Proprietor,Phone,Location,RegistrationDate")] ShopRegistration shopRegistration)
        {
            if (ModelState.IsValid)
            {
                db.ShopRegistrations.Add(shopRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shopRegistration);
        }

        // GET: ShopRegistrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopRegistration shopRegistration = db.ShopRegistrations.Find(id);
            if (shopRegistration == null)
            {
                return HttpNotFound();
            }
            return View(shopRegistration);
        }

        // POST: ShopRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShopRegistrationID,UserName,Password,ShopName,Proprietor,Phone,Location,RegistrationDate")] ShopRegistration shopRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shopRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shopRegistration);
        }

        // GET: ShopRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopRegistration shopRegistration = db.ShopRegistrations.Find(id);
            if (shopRegistration == null)
            {
                return HttpNotFound();
            }
            return View(shopRegistration);
        }

        // POST: ShopRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShopRegistration shopRegistration = db.ShopRegistrations.Find(id);
            db.ShopRegistrations.Remove(shopRegistration);
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
