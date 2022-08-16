using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMq_Connection;
using RabbitMq_learn.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_learn.Router.Topic
{
    public class Topic2Producer:IMessageProducer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName;
        private readonly string _routingKey;


        public Topic2Producer(string exchangeName, string routingkey)
        {
            // 创建连接对象
            _connection = MqHelper.GetConnection();

            // 创建信道
            _channel = _connection.CreateModel();

            _exchangeName = exchangeName;

            _routingKey = routingkey;
        }



        public void SendMessage<T>(T message)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = false;

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(_exchangeName, _routingKey, properties, body);
        }

        public void SendMessage<T>(T message,  Dictionary<string, object> headers)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = false;
            properties.Headers = headers;

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(_exchangeName, _routingKey, properties, body);
        }
    }
}
