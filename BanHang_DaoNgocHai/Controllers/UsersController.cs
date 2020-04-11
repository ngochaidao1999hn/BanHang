using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanHang_DaoNgocHai.Models;

namespace BanHang_DaoNgocHai.Controllers
{
    public class UsersController : Controller
    {
        private BanHangContext db = new BanHangContext();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user)
        {
            if (ModelState.IsValid) {
                var account = db.Users.Where(a => a.UserEmail == user.UserEmail && a.PassWord == user.PassWord).FirstOrDefault();
                if (account != null) {
                    Session["UserEmail"] = account.UserEmail;
                    return Redirect("/Home/Index");
                }
            }
            return View(user);
        }
        public ActionResult DangKy() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(Users user) {
            if (ModelState.IsValid) {

            }
            return View(user);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,UserEmail,PassWord,Role")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,UserEmail,PassWord,Role")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Users users = await db.Users.FindAsync(id);
            db.Users.Remove(users);
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
    }
}
