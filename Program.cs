using MQTTnet;
using MQTTnet.Server;
using System.Text;

Console.WriteLine("Mqtt Broker is starting!");

// Create the options for MQTT Broker
var options = new MqttServerOptionsBuilder()
    .WithDefaultEndpoint();

// Create a new mqtt server
var server = new MqttFactory().CreateMqttServer(options.Build());
server.InterceptingPublishAsync += Server_InterceptingPublishAsync;

// Start the server
await server.StartAsync();

// Keep application running until user press a key
Console.ReadLine();

static Task Server_InterceptingPublishAsync(InterceptingPublishEventArgs arg)
{
    // Convert Payload to string
    Console.WriteLine(" TimeStamp: {0} -- Message: ClientId = {1}, Topic = {2}, Payload = {3}, QoS = {4}, Retain-Flag = {5}",
        DateTime.Now,
        arg.ClientId,
        arg.ApplicationMessage?.Topic,
        Encoding.UTF8.GetString(arg.ApplicationMessage?.Payload),
        arg.ApplicationMessage?.QualityOfServiceLevel,
        arg.ApplicationMessage?.Retain);
    return Task.CompletedTask;
}
