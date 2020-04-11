using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHang_DaoNgocHai.Models
{
    public class Brands
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}