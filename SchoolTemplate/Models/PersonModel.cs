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
        [Required]
        public string Voornaam { set; get; }
        
        [Required]
        public string Achternaam { set; get; }
    }
}
