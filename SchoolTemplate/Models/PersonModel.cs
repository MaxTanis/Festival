using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System;
using MySql.Data.MySqlClient;

namespace SchoolTemplate.Models
{
    public class PersonModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht")]
        public string Voornaam { set; get; }

        [Required(ErrorMessage = "Achternaam is verplicht")]
        public string Achternaam { set; get; }

        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress]
        public string Email { set; get; }

        [Required(ErrorMessage = "Geboorte datum is verplicht")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Datetime")]
        public string Geb_datum { set; get; }
    }

    
}
