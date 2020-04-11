using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHang_DaoNgocHai.Models
{
    public class Categories
    {
        [Key]
        public int CateId { get; set; }
        public string CateName { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}