using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployee.Data
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal HourlyWage { get; set; }
        public virtual Address Address { get; set; }
        public ICollection<Tag> Tags { get; set; } 
    }
}
