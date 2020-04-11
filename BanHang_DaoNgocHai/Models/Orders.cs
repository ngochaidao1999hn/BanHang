using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHang_DaoNgocHai.Models
{
    public class Orders
    {
        [Key]
        public int OrdId { get; set; }
        public DateTime Date { get; set; }

        public int ClientsId { get; set; }
        public int status { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        [ForeignKey("ClientsId")]
        public virtual Clients Clients { get; set; }
    }
}