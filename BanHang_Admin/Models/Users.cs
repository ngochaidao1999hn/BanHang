namespace BanHang_Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [Key]
        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public string PassWord { get; set; }

        public string Role { get; set; }
    }
}
