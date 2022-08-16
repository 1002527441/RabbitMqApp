using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMq_Connection;
using RabbitMq_learn.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_learn.Router.Fanout
{
    public class Direct2Producer:IMessageProducer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private  string _exchangeName;
        private string _type;

        public Direct2Producer(string exchangeName)
        {
            // 创建连接对象
            _connection = MqHelper.GetConnection();

            // 创建信道
            _channel = _connection.CreateModel();
          
        }

        public void SetExchange(string exchangeName, string type)
        {
             _exchangeName = exchangeName;
            _type = type;

            // 声明交换机
            _channel.ExchangeDeclare(_exchangeName, type, false, false, null);
        }

        public void SetQueue(string queueName)
        {
            // 创建队列
            _channel.QueueDeclare(queueName, false, false, false, null);
        }

        public void Bind(string queueName, string exchangeName)
        {
            //把队列绑定到交换机
            _channel.QueueBind(queueName, exchangeName, "", null);
        }

        public void SendMessage<T>(T message)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = false;

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(_exchangeName, "", properties, body);
        }
    }
}
