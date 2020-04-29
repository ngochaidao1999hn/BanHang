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
using System.Data.SqlClient;

namespace BanHang_DaoNgocHai.Controllers
{
    public class OrdersController : Controller
    {
        private BanHangContext db = new BanHangContext();
        
        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var orders = db.Orders.Include(o => o.Clients).Include(o => o.OrderDetails);
            return View(await orders.ToListAsync());
        }
        [HttpGet]
        public ActionResult Order(int ProId, string size)
        {
            if (Session["ClientId"] != null)
            {
                //try
                //{
                //    string date = DateTime.Now.ToString();
                //    int ClientId = Int32.Parse(Session["ClientId"].ToString());
                //    var Date = new SqlParameter("@date", date);
                //    var Client = new SqlParameter("@clientid", ClientId);
                //    db.Database.ExecuteSqlCommand("Insert into Orders(Date,ClientsId)values('@date',@clientid)",Date,Client);
                //    // return Redirect("/OrderDetails/SetOrder?Quantities=" + Quantities + "&ProId=" + ProId + "");
                //    return Redirect("/Home/Index");
                //}
                //catch (Exception) {

                //}
                try
                {
                    db.Orders.Add(new Orders()
                    {
                        Date = DateTime.Now,
                        ClientsId = Int32.Parse(Session["ClientId"].ToString()),
                        status = 0
                    });
                    db.SaveChanges();
                    var ordid = db.Orders.Where(o => o.ClientsId == Int32.Parse(Session["ClientId"].ToString())).LastOrDefault().OrdId;
                    OrderDetailsController ord = new OrderDetailsController();
                    ord.ListOrd.Add(new OrderDetails()
                    {
                        ProId = ProId,
                        Size = size,
                        Quantities = 1,
                        OrderId = ordid

                    });
                }
                catch(Exception e) {

                }
               
               
            }
            return Redirect("/Home/Index");
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.ClientsId = new SelectList(db.Clients, "ClientId", "ClientName");
            ViewBag.OrdId = new SelectList(db.OrderDetails, "OrderId", "OrderId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrdId,Date,ClientsId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClientsId = new SelectList(db.Clients, "ClientId", "ClientName", orders.ClientsId);
            ViewBag.OrdId = new SelectList(db.OrderDetails, "OrderId", "OrderId", orders.OrdId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientsId = new SelectList(db.Clients, "ClientId", "ClientName", orders.ClientsId);
            ViewBag.OrdId = new SelectList(db.OrderDetails, "OrderId", "OrderId", orders.OrdId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrdId,Date,ClientsId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClientsId = new SelectList(db.Clients, "ClientId", "ClientName", orders.ClientsId);
            ViewBag.OrdId = new SelectList(db.OrderDetails, "OrderId", "OrderId", orders.OrdId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Orders orders = await db.Orders.FindAsync(id);
            db.Orders.Remove(orders);
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