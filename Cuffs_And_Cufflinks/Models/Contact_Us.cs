using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cuffs_And_Cufflinks.Models
{
    public class Contact_Us
    {


        [Required(ErrorMessage = "Name is Required.")]
        public String Name { set; get; }

        [Required(ErrorMessage = "Email is Required.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Mail Format is not Correct.")]
        public String Email { set; get; }

        [Required(ErrorMessage = "Subject is Required.")]
        public String Subject { set; get; }

        [Required(ErrorMessage = "Message is Required.")]
        public String Message { set; get; }
    }
}