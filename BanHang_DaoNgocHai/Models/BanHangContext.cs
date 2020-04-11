using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BanHang_DaoNgocHai.Models
{
    public class BanHangContext:DbContext
    {
        public BanHangContext() : base("DefaultConnection")
        {

        }
      public  DbSet<Categories> Categories { get; set; }
      public  DbSet<Products> Products { get; set; }
      public  DbSet<Orders> Orders { get; set; }
      public   DbSet<OrderDetails> OrderDetails { get; set; }
     public   DbSet<Clients> Clients { get; set; }
      public   DbSet<Users> Users { get; set; }
        public DbSet<Brands> Brands { get; set; }
    }
}