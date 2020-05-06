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
                if (Session["OrdId"] == null)
                {
                    db.Orders.Add(new Orders()
                    {
                        Date = DateTime.Now,
                        ClientsId = int.Parse(Session["ClientId"].ToString()),
                        status = 0
                    });
                    db.SaveChanges();
                    ///Chuwa sua dc order                   

                    return RedirectToAction("GetOrdId", new { ProId = ProId, size = size });

                }
                else
                {
                    return RedirectToAction("PutProductToOrderDetail", new { ProId = ProId, size = size });
                }
               
               
            }
            return Redirect("/Home/Index");
        }
        public ActionResult GetOrdId(int ProId, string size) {
            try
            {
                int ClientId = int.Parse(Session["ClientId"].ToString());
                var data = db.Orders.Where(o => o.ClientsId == ClientId ).Max(o=>o.OrdId);


                Session["OrdId"] = data;
                return RedirectToAction("PutProductToOrderDetail", new { ProId = ProId, size = size });
            }
            catch (Exception e) {
               // e.Message;
                return Redirect("~/Detail/Index?id=" + ProId);
            }
        }
        public ActionResult PutProductToOrderDetail(int ProId,string size) {
            if (Session["OrdId"] != null) {
                db.OrderDetails.Add(new OrderDetails() {
                    OrderId=int.Parse(Session["OrdId"].ToString()),
                    ProId=ProId,
                    Size = size,
                    Quantities=1
                });
                db.SaveChanges();
                return Redirect("~/Home/Index");
            }
            return Redirect("~/Detail/Index?id=" + ProId);
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
