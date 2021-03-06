﻿using System;
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
    public class OrderDetailsController : Controller
    {
        private BanHangContext db = new BanHangContext();
        public List<OrderDetails> ListOrd = new List<OrderDetails>();
        // GET: OrderDetails
        public async Task<ActionResult> Index()
        {
            int ordId = int.Parse(Session["OrdId"].ToString());
            var orderDetails = db.OrderDetails.Include(o => o.Products).Where(o => o.OrderId == ordId).ToListAsync();
            if (orderDetails != null)
            {
                return View(await orderDetails);
            }
            else {
                return Redirect("~/Orders/Remove?ordId=" + Session["OrdId"]);
            }
        }
        public ActionResult IndexAdmin() {
            var orderDetails = db.OrderDetails.Include(o => o.Products);
            return View(orderDetails.ToList());
        }
        //+1 vao quantities
        public ActionResult IncreaseQuantities(int OrdDetailId) {
            var data = db.OrderDetails.Where(o => o.Id == OrdDetailId).First();
            if (data != null)
            {
                //OrderDetails ord = new OrderDetails()
                //{
                //    Id = OrdDetailId,
                //    OrderId = data.OrderId,
                //    ProId = data.ProId,
                //    Quantities = data.Quantities + 1,
                //    Size = data.Size
                //};

                //    db.Entry(ord).State = EntityState.Modified;
                //    db.SaveChanges();
                int newquantities = data.Quantities + 1;
                db.Database.ExecuteSqlCommand("Update OrderDetails set Quantities ="+newquantities+" where Id ="+data.Id);
                    return RedirectToAction("Index");
               
               
            }
            return RedirectToAction("Index");
        
    }
        //-1 vao quantities
        public ActionResult ReduceQuantities(int OrdDetailId) {
            var data = db.OrderDetails.Where(o => o.Id == OrdDetailId).FirstOrDefault();
            if (data != null)
            {

                //db.Entry(new OrderDetails()
                //{
                //    Id = OrdDetailId,
                //    OrderId = data.OrderId,
                //    ProId = data.ProId,
                //    Quantities = data.Quantities -1,
                //    Size = data.Size
                //}).State = EntityState.Modified;
                //db.SaveChanges();
                int newquantities = data.Quantities - 1;
                if (newquantities <= 0)
                {
                    db.Database.ExecuteSqlCommand("delete from OrderDetails where Id =" + data.Id);
                }
                else
                {
                    db.Database.ExecuteSqlCommand("Update OrderDetails set Quantities =" + newquantities + " where Id =" + data.Id);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveItem(int OrdDetailId) {
            OrderDetails o = db.OrderDetails.Where(ord => ord.Id == OrdDetailId).FirstOrDefault();
            db.OrderDetails.Remove(o);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: OrderDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = await db.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "OrdId", "OrdId");
            ViewBag.ProId = new SelectList(db.Products, "ProId", "ProName");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderId,ProId,Size,Quantities")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetails);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "OrdId", "OrdId", orderDetails.OrderId);
            ViewBag.ProId = new SelectList(db.Products, "ProId", "ProName", orderDetails.ProId);
            return View(orderDetails);
        }

        // GET: OrderDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = await db.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrdId", "OrdId", orderDetails.OrderId);
            ViewBag.ProId = new SelectList(db.Products, "ProId", "ProName", orderDetails.ProId);
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,ProId,Size,Quantities")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetails).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrdId", "OrdId", orderDetails.OrderId);
            ViewBag.ProId = new SelectList(db.Products, "ProId", "ProName", orderDetails.ProId);
            return View(orderDetails);
        }

        // GET: OrderDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = await db.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }
        public ActionResult SetOrder(int Quantities,int ProId) {
            OrderDetails OD = new OrderDetails();
            OD.OrderId = db.Orders.Max(p => p.OrdId);
            OD.ProId = ProId;
            OD.Quantities = Quantities;
            db.OrderDetails.Add(OD);
            return Redirect("/Home/Index");
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderDetails orderDetails = await db.OrderDetails.FindAsync(id);
            db.OrderDetails.Remove(orderDetails);
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
