using Confluent.Kafka;
using Common.Message;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Common.Helper;

public class MessagePublisher
{
    private IProducer<Null, string> _producer;

    private ILogger _logger;

    public MessagePublisher(IConfiguration configuration, ILogger<MessagePublisher> logger)
    {
        _logger = logger;
        var config = new ProducerConfig
        {
            BootstrapServers = configuration.GetConnectionString("Kafka:BootstrapServers") ?? "localhost:9092",
        };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public void SendMessage<T>(string topic, T message) where T : AbstractMessage
    {
       var result = _producer.ProduceAsync(topic, new Message<Null, string> { Value = JsonSerializer.Serialize(message) }).Result;
        _logger.LogInformation($"{result.Topic} : {result.Message.Value}");
    }
}

