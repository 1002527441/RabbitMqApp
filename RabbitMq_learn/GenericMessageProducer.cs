using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMq_Connection;
using RabbitMq_learn.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_learn
{


    public class GenericMessageProducer:IGenericMessageProducer
    {

        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly string _QueueName;
        private readonly string _routingKey;

        public GenericMessageProducer()
        {
            // 创建连接对象
            _connection = MqHelper.GetConnection();
            // 创建信道
            _channel = _connection.CreateModel();
        }

        public void Bind(string queueName, string exchangeName, string routingKey="")
        {
           _channel.QueueBind(queueName, exchangeName, routingKey, null);
        }


        public void SetExchange(string exchangeName, string type)
        {
            _channel.ExchangeDeclare(exchangeName, type, false, false,  null);
        }

        public void SetQueue(string QueueName)
        {
            _channel.QueueDeclare(QueueName, false, false, false, null);
        }

        public void SendMessage<T>(string exchangeName, string routingkey, T message)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = false;

            var body = GetBytes(message);
            _channel.BasicPublish(exchangeName, routingkey, false, basicProperties: properties, body);
        }

        public void SendMessage<T>(string exchangeName, string routingkey, Dictionary<string, object> headers, T message)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = false;
            properties.Headers = headers;

            var body = GetBytes(message);
            _channel.BasicPublish(exchangeName, routingkey, false, basicProperties: properties, body);
        }

        public void SendMessageByExchange<T>(string exchangeName, T message)
        {      
            var body = GetBytes(message);
            _channel.BasicPublish(exchange: exchangeName, routingKey: "", mandatory: false, null, body);
        }

        public void SendMessageByRoutingKey<T>(string routingkey, T message)
        {      
            // 创建队列
            _channel.QueueDeclare(routingkey, false, false, false, null);

            var body = GetBytes(message);

            _channel.BasicPublish(exchange: "", routingkey, false, null, body);
        }


        private byte[] GetBytes<T>(T message)
        {
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}
