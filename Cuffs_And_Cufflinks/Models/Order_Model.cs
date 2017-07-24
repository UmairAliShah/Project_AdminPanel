using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cuffs_And_Cufflinks.Models
{
    public class Order_Model
    {
        [Required(ErrorMessage = "First Name is Required.")]
        public String First_Name { get; set; }

        [Required(ErrorMessage = "Last Name is Required.")]
        public String Last_Name { get; set; }

        [Required(ErrorMessage = "Email is Required.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Mail Format is not Correct.")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Phone is Required.")]
        public String Phone { get; set; }

        [Required(ErrorMessage = "City is Required.")]
        public String City { get; set; }

        [Required(ErrorMessage = "Address is Required.")]
        public String Address { get; set; }

    }
}