namespace BlazorEmployee.Data
{
    public class EmployeeService
    {
     
        public List<Employee> employees = new List<Employee>
        {
             new Employee
            {
                Id = 1,
                FirstName = "Calle",
                LastName = "Svensson",
                BirthDate = new DateTime(1991, 09, 01),
                HourlyWage = 100,
                Address = new Address
                {
                    Id = 1,
                    Street = "Bäckvägen 38",
                    City = "Stockholm",
                    Zip = 12637
                }
            },
             new Employee
            {
                Id = 2,
                FirstName = "Behrouz",
                LastName = "Talebi",
                BirthDate = new DateTime(1983, 05, 22),
                HourlyWage = 1000,
                Address = new Address
                {
                    Id = 2,
                    Street = "Avenyn",
                    City = "Göteborg",
                    Zip = 40015
                }
            }
        };

        public List<Employee> GetEmployees()
        {

            return employees;
        }
        public void NewEmployee(Employee newEmployee)
            {
                int id = SetId();
                employees.Add(new Employee
                {
                    Id = id,
                    FirstName = newEmployee.FirstName,
                    LastName = newEmployee.LastName,
                    BirthDate = newEmployee.BirthDate,
                    HourlyWage = newEmployee.HourlyWage,
                    Address = new Address
                    {
                        Id = id,
                        Street = newEmployee.Address.Street,
                        City = newEmployee.Address.City,
                        Zip = newEmployee.Address.Zip
                    }
                });
            }
    
        public int SetId()
        {
            int id = 0;
            do
            {
                Random rnd = new Random();
                id = rnd.Next(1, 20);
            } while (
                employees.Any(e => e.Id == id)

            );
            return id;
        }

        public Employee GetEmployee(int id)
        {
            return employees.SingleOrDefault(employee => employee.Id == id);
        }

        public void UpdateEmployee(Employee employee)
        {

            Employee employeeToUpdate = employees.Find(e => e.Id == employee.Id);
            employeeToUpdate = employee;
        }

        public void DeleteEmployee(Employee employee)
        {
            employees.Remove(employee);
        }



    }
}
