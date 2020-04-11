using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHang_DaoNgocHai.Models
{
    public class Products
    {
        [Key]
      public  int ProId { get; set; }
        [Required]
        public string ProName { get; set; }
        [Required]
        public double Price { get; set; }
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string Url { get; set; }
        [Required]
        public char status { get; set; }
        public int CateId { get; set; }
        public int BrandId { get; set; }
        [ForeignKey("CateId")]
        public virtual Categories Categories { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brands Brands { get; set;}
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}