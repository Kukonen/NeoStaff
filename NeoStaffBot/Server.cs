using NeoStaffBot.Model;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace NeoStaffBot
{
    internal class Server
    {
        public static async Task<EmployeeSpecification[]> GetLastSpecificationAsync()
        {
            // TODO обращение к серверу
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("http://api:80/api/staff/notification");
                    //HttpResponseMessage response = await client.GetAsync("http://localhost:8080/api/staff/notification");

                    // Проверяем успешность запроса
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        string unescapedBody = Regex.Unescape(responseBody);

                        List<EmployeeSpecification> objectsList = new List<EmployeeSpecification>();

                        try
                        {
                            JsonDocument jsonDocument = JsonDocument.Parse(unescapedBody);


                            foreach (JsonElement element in jsonDocument.RootElement.EnumerateArray())
                            {
                                var employeeSpecification = new EmployeeSpecification();

                                foreach (JsonProperty item in element.EnumerateObject())
                                {
                                    switch (item.Name)
                                    {
                                        case "serviceNumber":
                                            {
                                                employeeSpecification.ServiceNumber = item.Value.ToString();
                                                break;
                                            }

                                        case "surname":
                                            {
                                                employeeSpecification.Surname = item.Value.ToString();
                                                break;
                                            }

                                        case "name":
                                            {
                                                employeeSpecification.Name = item.Value.ToString();
                                                break;
                                            }

                                        case "middlename":
                                            {
                                                employeeSpecification.Middlename = item.Value.ToString();
                                                break;
                                            }

                                        case "lastCertification":
                                            {
                                                if (item.Value.ToString().Equals(""))
                                                {
                                                    employeeSpecification.LastCertification = DateOnly.ParseExact("0001-01-01", "yyyy-MM-dd", null);
                                                }
                                                else
                                                {
                                                    employeeSpecification.LastCertification = DateOnly.ParseExact(item.Value.ToString(), "yyyy-MM-dd", null);
                                                }
                                                break;
                                            }

                                        default:
                                            {
                                                throw new Exception();
                                            }
                                    }
                                }

                                objectsList.Add(employeeSpecification);
                            }

                            return objectsList.ToArray();
                        }
                        catch (Exception)
                        {
                            return objectsList.ToArray();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: " + response.StatusCode);
                        return null;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка при выполнении запроса: " + e.Message);
                    return null;
                }
            }
        }
    }
}
