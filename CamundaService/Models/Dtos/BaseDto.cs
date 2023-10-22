using System.Text.Json.Serialization;

namespace CamundaService.Models.Dtos;

public class BaseDto
{
    [JsonPropertyName("id")] 
    public string? Id { get; set; }
}