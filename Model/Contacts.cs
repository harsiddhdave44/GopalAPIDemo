using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GopalAPIDemo.Model
{
    [Alias("Contacts")]
    public class Contacts
    {
        [AutoIncrement]
        public int Id;
        
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = false, ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = false, ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = false, ErrorMessage = "ContactNumber is required")]
        public string ContactNumber { get; set; }

        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = false, ErrorMessage = "CityOfResidence is required")]
        public string CityOfResidence { get; set; }
    }
}
