using BanHang_DaoNgocHai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
namespace BanHang_DaoNgocHai.Controllers
{
    public class HomeController : Controller
    {
        private BanHangContext db = new BanHangContext();
        public ActionResult Index()
        {
            var productList = db.Products.Include(c => c.Categories);
            return View(productList.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}