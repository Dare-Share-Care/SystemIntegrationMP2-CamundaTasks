using System.Text;
using System.Text.Json;
using CamundaService.Camunda;
using CamundaService.Models.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost" };
var connection = factory.CreateConnection();
using var channel = connection.CreateChannel();

channel.QueueDeclare("camunda");
Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var dto = JsonSerializer.Deserialize<CompleteTaskDto>(message);
    
    //Complete Camunda Task
    var camundaTask = new CamundaTask();
    camundaTask.CompleteTask(dto);
};

channel.BasicConsume(queue: "camunda", autoAck: true, consumer: consumer);
Console.WriteLine(" Press [enter] to exit.");
Console.ReadKey();