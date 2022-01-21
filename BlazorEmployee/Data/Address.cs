using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployee.Data
{
    public class Address
    {
        [ForeignKey("Employee")]
        public int Id { get; set; } 
        public string Street { get; set; }
        public string City { get; set; }
        [Range(10000,99999)]
        public int Zip { get; set; }
        public Employee Employee { get; set; }
    }
}
