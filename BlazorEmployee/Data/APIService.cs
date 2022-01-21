using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BlazorEmployee.Data
{
    public class APIService
    {
        private readonly HttpClient httpClient;

        public APIService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public string GetBaseUrl()
        {
            return httpClient.BaseAddress.ToString();
        }

        public async Task<ICollection<Employee>> GetEmployeesAsync()
        {
            ICollection<Employee> employees = new List<Employee>();
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://localhost:44339/api/Employee");

            var response = await httpClient.SendAsync(request);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            using var responseStream = await response.Content.ReadAsStreamAsync();
            employees = await JsonSerializer.DeserializeAsync
                <ICollection<Employee>>(responseStream, options);
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            Employee employee = new Employee();
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://localhost:44339/api/Employee/{id}");

            

            var response = await httpClient.SendAsync(request);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            using var responseStream = await response.Content.ReadAsStreamAsync();
            employee = await JsonSerializer.DeserializeAsync
                <Employee>(responseStream, options);
            return employee;
        }

        public async Task CreateEmployee([FromForm] Employee employee)
        {
            var employeeJson = new StringContent(
                JsonSerializer.Serialize(employee),
                Encoding.UTF8,
                "application/json");

            using var httpResponseMessage =
                await httpClient.PostAsync("https://localhost:44339/api/Employee/Create", employeeJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task UpdateEmployee([FromForm]Employee employee)
        {
            var employeeJson = new StringContent(
                JsonSerializer.Serialize(employee),
                Encoding.UTF8,
                "application/json");

            using var httpResponseMessage =
                await httpClient.PostAsync("https://localhost:44339/api/Employee/Update", employeeJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteEmployeeAsync(int id)
        {

            using var httpResponseMessage =
                await httpClient.DeleteAsync($"https://localhost:44339/api/Employee/Delete/?id={id}");

            httpResponseMessage.EnsureSuccessStatusCode();

        }
    }
}
