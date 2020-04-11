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
    public class DetailController : Controller
    {
        private BanHangContext db = new BanHangContext();
        // GET: Detail
        public ActionResult Index(int? id)
        {
            var product = db.Products.Include(p => p.Categories).Where(pr => pr.ProId == id);
            return View(product.ToList());
        }
    }
}