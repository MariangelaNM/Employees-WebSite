using Employees_Front.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Employees_Front.Services
{
    public class Service_API_Department : IService_API_Department
    {
        private static string _baseUrl;

        public Service_API_Department()
        {
            // Read the base URL from the appsettings.json file during object creation.
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
        }

        public async Task<List<Department>> GetDepartment()
        {
            List<Department> list = new List<Department>();

            // Using statement ensures that the HttpClient is disposed of properly.
            using (var client = new HttpClient())
            {
                try
                {
                    // Set the base address for the HttpClient.
                    client.BaseAddress = new Uri(_baseUrl);

                    // Make an asynchronous GET request to the "api/Department" endpoint.
                    var response = await client.GetAsync("api/Department");

                    if (response.IsSuccessStatusCode)
                    {
                        // If the response is successful, read the content as a string.
                        var json_response = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into a List of Department objects.
                        list = JsonConvert.DeserializeObject<List<Department>>(json_response);
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

            // Return the list of departments.
            return list;
        }
        public async Task<Department> GetDepartmentById(int departmentID)
        {
            Department departmentList = new Department();

            // Using statement ensures that the HttpClient is disposed of properly.
            using (var client = new HttpClient())
            {
                try
                {
                    // Set the base address for the HttpClient.
                    client.BaseAddress = new Uri(_baseUrl);

                    // Make an asynchronous GET request to the "api/Department" endpoint.
                    var response = await client.GetAsync("api/Department/" + departmentID);

                    if (response.IsSuccessStatusCode)
                    {
                        // If the response is successful, read the content as a string.
                        var json_response = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into a List of Department objects.
                        departmentList = JsonConvert.DeserializeObject<Department>(json_response);
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

            // Return the list of departments.
            return departmentList;
        }
        public async Task<bool> Post(Department department)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Set the base address for the HttpClient.
                    client.BaseAddress = new Uri(_baseUrl);

                    // Convert the Department object to JSON.
                    var json_data = JsonConvert.SerializeObject(department);

                    // Set the content type header to indicate that the content is JSON.
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    // Make an asynchronous POST request to the "api/Department" endpoint.
                    var response = await client.PostAsync("api/Department", new StringContent(json_data, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle validation errors
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        var errorDetails = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(errorResponse);

                        foreach (var keyValuePair in errorDetails)
                        {
                            foreach (var errorMessage in keyValuePair.Value)
                            {
                                Console.WriteLine($"Error in {keyValuePair.Key}: {errorMessage}");
                            }
                        }

                        return false;
                    }
                    else
                    {
                        // Handle other errors
                        Console.WriteLine($"Error: {response.ReasonPhrase}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    // Handle other exceptions.
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }


        public async Task<bool> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Set the base address for the HttpClient.
                    client.BaseAddress = new Uri(_baseUrl);

                    // Make an asynchronous DELETE request to the "api/Department/{id}" endpoint.
                    var response = await client.DeleteAsync($"api/Department/{id}");

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



        public async Task<bool> Edit(Department department)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Set the base address for the HttpClient.
                    client.BaseAddress = new Uri(_baseUrl);

                    // Convert the Department object to JSON.
                    var json_data = JsonConvert.SerializeObject(department);

                    // Set the content type header to indicate that the content is JSON.
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    // Make an asynchronous POST request to the "api/Department" endpoint.
                    var response = await client.PutAsync("api/Department/" + department.DepartmentID, new StringContent(json_data, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle validation errors
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        var errorDetails = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(errorResponse);

                        foreach (var keyValuePair in errorDetails)
                        {
                            foreach (var errorMessage in keyValuePair.Value)
                            {
                                Console.WriteLine($"Error in {keyValuePair.Key}: {errorMessage}");
                            }
                        }

                        return false;
                    }
                    else
                    {
                        // Handle other errors
                        Console.WriteLine($"Error: {response.ReasonPhrase}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    // Handle other exceptions.
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }

    }
}
