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

    public void CompleteTask(CompleteTaskDto dto)
    {
        var url = $"http://localhost:8080/engine-rest/task/{dto.Id}/complete";
        var dtoJson = JsonSerializer.Serialize(dto, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        var content = new StringContent(dtoJson, Encoding.UTF8, "application/json");
        var response = _httpClient.PostAsync(url, content);
        var result = response.Result.Content.ReadAsStringAsync();
        if(response.IsCompletedSuccessfully && response.Result.StatusCode == HttpStatusCode.NoContent)
        {
            Console.WriteLine("Task completed");
        }
        else
        {
            Console.WriteLine($"Error: {result.Result}");
        }
    }
}