using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHang_DaoNgocHai.Models
{
    public class Clients
    {
        [Key]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string ClientEmail { get; set; }
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string PassWord { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("PassWord")]
        public string RePassWord { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        public string Adress { get; set; }
        public ICollection<Orders> Orders { get; set; }

    }
}