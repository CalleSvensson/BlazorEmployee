using System.Collections.Generic;

namespace BlazorEmployee.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected = false;

        public ICollection<Employee> Employees { get; set; }
    }

}