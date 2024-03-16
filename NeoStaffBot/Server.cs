using NeoStaffBot.Model;
using System.Text.Json;

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
                    HttpResponseMessage response = await client.GetAsync("localhost:8080/api/staff/notification");
                    Console.WriteLine(response.ToString());

                    // Проверяем успешность запроса
                    if (response.IsSuccessStatusCode)
                    {
                        // Читаем содержимое ответа
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                        try
                        {
                            return JsonSerializer.Deserialize<EmployeeSpecification[]>(responseBody);
                        } catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка: " + ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: " + response.StatusCode);
                        return null;
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Ошибка при выполнении запроса: " + e.Message);
                    return null;
                }
            }
        }
    }
}
