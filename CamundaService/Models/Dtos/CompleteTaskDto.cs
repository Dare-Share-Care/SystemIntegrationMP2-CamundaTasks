using System.Text.Json.Serialization;

namespace CamundaService.Models.Dtos;

public class CompleteTaskDto : BaseDto
{
    [JsonPropertyName("variables")]
    public Dictionary<string, Dictionary<string, object>>? Variables { get; set; } 
}