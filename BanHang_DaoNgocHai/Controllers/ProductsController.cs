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
using System.IO;

namespace BanHang_DaoNgocHai.Controllers
{
    public class ProductsController : Controller
    {
        private BanHangContext db = new BanHangContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Categories).Include(p=>p.Brands);
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CateId = new SelectList(db.Categories, "CateId", "CateName");
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProId,ProName,Url,Price,CateId,BrandId")] Products products, HttpPostedFileBase Url)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Url != null) {
                        string path = Path.Combine(Server.MapPath("~/images"),Path.GetFileName(Url.FileName));
                        Url.SaveAs(path);
                        
                    }
                }
                catch (Exception e) {
                    ViewBag.FileStatus = "Error while file uploading.";
                }
                products.Url = Path.GetFileName(Url.FileName);
                db.Products.Add(products);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CateId = new SelectList(db.Categories, "CateId", "CateName", products.CateId);
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName",products.BrandId);
            return View(products);
        }


        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CateId = new SelectList(db.Categories, "CateId", "CateName", products.CateId);
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName",products.BrandId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProId,ProName,Price,Url,CateId,BrandId")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CateId = new SelectList(db.Categories, "CateId", "CateName", products.CateId);
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName",products.BrandId);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Products products = await db.Products.FindAsync(id);
            db.Products.Remove(products);
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
