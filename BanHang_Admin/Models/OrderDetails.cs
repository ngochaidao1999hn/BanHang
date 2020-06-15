namespace BanHang_Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetails
    {
        public int OrderId { get; set; }

        public int ProId { get; set; }

        public int Quantities { get; set; }

        [Required]
        public string Size { get; set; }

        public int Id { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Products Products { get; set; }
    }
}
