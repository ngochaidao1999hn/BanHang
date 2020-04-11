using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHang_DaoNgocHai.Models
{
    public class OrderDetails
    {

        [Key]
        public int OrderId { get; set; }

        public int ProId { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public int Quantities { get; set; }
        [ForeignKey("OrderId")]
        public virtual Orders Orders { get; set; }
        [ForeignKey("ProId")]
        public virtual Products Products { get; set; }
     
    }
}