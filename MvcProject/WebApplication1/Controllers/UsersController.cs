using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        private MvcProjectDBEntities1 db = new MvcProjectDBEntities1();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.TblUsers.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUser tblUser = await db.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,Username,UserPass,UserType")] TblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                var userId = db.TblUsers.Max(o => o.UserID) + 1;
                var oTblUser = new TblUser();
                oTblUser.UserID = userId;
                oTblUser.Username = tblUser.Username;
                oTblUser.UserPass = tblUser.UserPass;
                oTblUser.UserType = tblUser.UserType;
                db.TblUsers.Add(oTblUser);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblUser);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUser tblUser = await db.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,Username,UserPass,UserType")] TblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblUser);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUser tblUser = await db.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TblUser tblUser = await db.TblUsers.FindAsync(id);
            db.TblUsers.Remove(tblUser);
            await db.SaveChangesAsync();
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
        //User Permission
        public async Task<ActionResult> UserRole(int userId)
        {
            var listTblUserRole = await db.TblUserRoles.Where(o => o.UserID == userId).ToListAsync();
            var oTblUser = await db.TblUsers.Where(o => o.UserID == userId).FirstOrDefaultAsync();
            TempData["Username"] = oTblUser.Username;

            #region create menu at view page
            var listUserRole = new List<TblUserRole>();

            #region Products
            var oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Products").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Products"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Category
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Categories").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Categories";    // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Customers
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Customers").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Customers"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Purchase
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Purchases").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Purchases"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Sales
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Sales").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Sales"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Stock
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Stocks").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Stocks"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Store
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Stores").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Stores"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Suppliers
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Suppliers").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Suppliers"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Warehouse
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Warehouses").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Warehouses"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region ShopRegistrations
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "ShopRegistrations").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "ShopRegistrations"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Home
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Home").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Home"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #endregion

            return View(listUserRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserRole(TblUserRole[] tblUserRoles)
        {
            var userRoleIdMax = db.TblUserRoles.Max(o => (int?)o.UserRoleID) ?? 0;
            for (var i = 0; i < tblUserRoles.Length; i++)
            {
                int userRoleId = tblUserRoles[i].UserRoleID;
                var oTblUserRole = await db.TblUserRoles.Where(o => o.UserRoleID == userRoleId).FirstOrDefaultAsync();
                if (oTblUserRole == null) // insert
                {
                    oTblUserRole = new TblUserRole();
                    oTblUserRole.UserRoleID = ++userRoleIdMax;
                    oTblUserRole.UserID = tblUserRoles[i].UserID;
                    oTblUserRole.PageName = tblUserRoles[i].PageName;
                    oTblUserRole.IsCreate = tblUserRoles[i].IsCreate;
                    oTblUserRole.IsRead = tblUserRoles[i].IsRead;
                    oTblUserRole.IsUpdate = tblUserRoles[i].IsUpdate;
                    oTblUserRole.IsDelete = tblUserRoles[i].IsDelete;
                    db.TblUserRoles.Add(oTblUserRole);
                }
                else // update
                {
                    oTblUserRole.IsCreate = tblUserRoles[i].IsCreate;
                    oTblUserRole.IsRead = tblUserRoles[i].IsRead;
                    oTblUserRole.IsUpdate = tblUserRoles[i].IsUpdate;
                    oTblUserRole.IsDelete = tblUserRoles[i].IsDelete;
                }
            }
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
