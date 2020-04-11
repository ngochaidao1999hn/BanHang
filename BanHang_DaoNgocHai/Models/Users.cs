using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHang_DaoNgocHai.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        public string Role { get; set; }
    }
}