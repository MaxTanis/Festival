using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SchoolTemplate.Models
{
    public class PersonModel
    {
        [Required(ErrorMessage = "Naam is verplicht")]
        public string Naam { set; get; }

        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress]
        public string Email { set; get; }
    }
}
