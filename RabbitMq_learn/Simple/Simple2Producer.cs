using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMq_Connection;
using RabbitMq_learn.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_learn.Simple
{
    public class Simple2Producer : IMessageProducer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _simpleQueueName;

        public Simple2Producer(string simpleQueueName)
        {
            // 创建连接对象
            _connection = MqHelper.GetConnection();

            // 创建信道
            _channel = _connection.CreateModel();

            // 创建队列
            _channel.QueueDeclare(simpleQueueName, false, false, false, null);

            _simpleQueueName = simpleQueueName;
        }
        public void SendMessage<T>(T message)
        {         
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                 _channel.BasicPublish(exchange: "", routingKey: _simpleQueueName, body: body);           
        }
    }
}
