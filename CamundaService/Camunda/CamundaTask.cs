using System.Net;
using System.Text;
using System.Text.Json;
using CamundaService.Models.Dtos;

namespace CamundaService.Camunda;

public class CamundaTask
{
    private readonly HttpClient _httpClient;

    public CamundaTask()
    {
        _httpClient = new HttpClient();
    }

    public async Task CompleteTask(CompleteTaskDto dto)
    {
        var url = $"http://localhost:8080/engine-rest/task/{dto.Id}/complete";
        var dtoJson = JsonSerializer.Serialize(dto, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        var content = new StringContent(dtoJson, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var result = response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Task completed");
        }
        else
        {
            Console.WriteLine("Error: " + result);
        }
    }
}