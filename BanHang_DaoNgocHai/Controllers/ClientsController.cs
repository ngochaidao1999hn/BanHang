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
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Validation;

namespace BanHang_DaoNgocHai.Controllers
{
    public class ClientsController : Controller
    {
        private BanHangContext db = new BanHangContext();

        // GET: Clients
        public async Task<ActionResult> Index()
        {
            return View(await db.Clients.ToListAsync());
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Clients client)
        {
            
                var account = db.Clients.Where(a => a.ClientEmail == client.ClientEmail  && a.PassWord == CreateMD5Hash(client.PassWord)).FirstOrDefault();
                if (account != null)
                {
                    Session["UserEmail"] = account.ClientName;
                    Session["ClientId"] = account.ClientId;
                    return Redirect("/Home/Index");
                }
            ViewBag.mess = "1";
            
            return View(client);
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(Clients client)
        {
            if (ModelState.IsValid)
            {
                if (db.Clients.Any(cl => cl.ClientEmail == client.ClientEmail))
                {
                    ViewBag.mess = "Tai Khoan da ton tai";
                    return View(client);
                }
                else {
                    try
                    {
                        client.PassWord = CreateMD5Hash(client.PassWord);
                        db.Clients.Add(client);
                        db.SaveChanges();
                        Session["UserEmail"] = client.ClientEmail;
                        Session["ClientId"] = client.ClientId;
                        return Redirect("/Home/Index");
                    }
                    catch (Exception e) {
                        return View(client);
                    }
                }
            }
            return View(client);
        }
        public ActionResult Logout() {
            Session["UserEmail"] = null;
            Session["ClientId"] = null;
            return Redirect("/Home/Index");
        }
        // GET: Clients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = await db.Clients.FindAsync(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ClientId,ClientName,ClientEmail,PassWord,Adress,PhoneNumber")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(clients);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(clients);
        }

        // GET: Clients/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = await db.Clients.FindAsync(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClientId,ClientName,ClientEmail,PassWord")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clients).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(clients);
        }

        // GET: Clients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = await db.Clients.FindAsync(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Clients clients = await db.Clients.FindAsync(id);
            db.Clients.Remove(clients);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public string CreateMD5Hash(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(input));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
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
