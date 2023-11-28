using Employees_Front.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Employees_Front.Services
{
    public class Service_API_Employee : IService_API_Employee
    {
        private static string _baseUrl;

        public Service_API_Employee()
        {
            // Read the base URL from the appsettings.json file during object creation.
              var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
        }

        public async Task<List<Employee>> GetEmployee()
        {
            List<Employee> list = new List<Employee>();

            // Using statement ensures that the HttpClient is disposed of properly.
            using (var client = new HttpClient())
            {
                try
                {
                    // Set the base address for the HttpClient.
                    client.BaseAddress = new Uri(_baseUrl);

                    // Make an asynchronous GET request to the "api/Employee" endpoint.
                    var response = await client.GetAsync("api/Employee");

                    if (response.IsSuccessStatusCode)
                    {
                        // If the response is successful, read the content as a string.
                        var json_response = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into a List of Employee objects.
                        list = JsonConvert.DeserializeObject<List<Employee>>(json_response);
                    }
                    else
                    {
                        // Handle the case of a non-successful HTTP response.
                        Console.WriteLine($"Error in the request: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // Handle other exceptions.
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            // Return the list of Employees.
            return list;
        }
        public async Task<Employee> GetEmployeeById(int id)
        {
            Employee employeeList = new Employee();

            // Using statement ensures that the HttpClient is disposed of properly.
            using (var client = new HttpClient())
            {
                try
                {
                    // Set the base address for the HttpClient.
                    client.BaseAddress = new Uri(_baseUrl);

                    // Make an asynchronous GET request to the "api/Employee" endpoint.
                    var response = await client.GetAsync("api/Employee/" + id);

                    if (response.IsSuccessStatusCode)
                    {
                        // If the response is successful, read the content as a string.
                        var json_response = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into a List of Employee objects.
                        employeeList = JsonConvert.DeserializeObject<Employee>(json_response);
                    }
                    else
                    {
                        // Handle the case of a non-successful HTTP response.
                        Console.WriteLine($"Error in the request: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // Handle other exceptions.
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            // Return the list of employees.
            return employeeList;
        }
        public async Task<(bool Success, string Message)> Post(Employee employee)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Set the base address for the HttpClient.
                    client.BaseAddress = new Uri(_baseUrl);

                    // Convert the employee object to JSON.
                    var json_data = JsonConvert.SerializeObject(employee);

                    // Set the content type header to indicate that the content is JSON.
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    // Make an asynchronous POST request to the "api/Employee" endpoint.
                    var response = await client.PostAsync("api/Employee", new StringContent(json_data, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Employee added successfully!");
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle validation errors
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        var errorDetails = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(errorResponse);

                        // Construir un mensaje de error combinando todas las validaciones.
                        var errorMessage = new StringBuilder();
                        foreach (var keyValuePair in errorDetails)
                        {
                            foreach (var errorMessagePart in keyValuePair.Value)
                            {
                                errorMessage.AppendLine($"Error in {keyValuePair.Key}: {errorMessagePart}");
                            }
                        }

                        return (false, errorMessage.ToString());
                    }
                    else
                    {
                        // Handle other errors
                        return (false, $"Error: {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    // Handle other exceptions.
                    return (false, $"Error: {ex.Message}");
                }
            }
        }



        public async Task<bool> Delete(int EmployeeID)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Set the base address for the HttpClient.
                    client.BaseAddress = new Uri(_baseUrl);

                    // Make an asynchronous DELETE request to the "api/Employee/{id}" endpoint.
                    var response = await client.DeleteAsync($"api/Employee/{EmployeeID}");

                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    // Handle other exceptions.
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }



        public async Task<(bool Success, string Message)> Edit(Employee employee)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Set the base address for the HttpClient.
                    client.BaseAddress = new Uri(_baseUrl);

                    // Convert the employee object to JSON.
                    var json_data = JsonConvert.SerializeObject(employee);

                    // Set the content type header to indicate that the content is JSON.
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    // Make an asynchronous POST request to the "api/Employee" endpoint.
                    var response = await client.PutAsync("api/Employee/" + employee.EmployeeID, new StringContent(json_data, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Employee saved successfully!");
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        var errorDetails = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(errorResponse);

                        // Construir un mensaje de error combinando todas las validaciones.
                        var errorMessage = new StringBuilder();
                        foreach (var keyValuePair in errorDetails)
                        {
                            foreach (var errorMessagePart in keyValuePair.Value)
                            {
                                errorMessage.AppendLine($"Error in {keyValuePair.Key}: {errorMessagePart}");
                            }
                        }

                        return (false, errorMessage.ToString());
                    }
                    else
                    {
                        // Handle other errors
                        Console.WriteLine($"Error: {response.ReasonPhrase}");
                        return (false, $"Error: {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    // Handle other exceptions.
                    Console.WriteLine($"Error: {ex.Message}");
                     return (false, $"Error: {ex.Message}");
                }
            }
        }

    }
}
