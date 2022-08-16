using RabbitMQ.Client;
using RabbitMq_Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_Consumer.Simple
{
    public class Simple2Consumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public Simple2Consumer(string simpleQueueName)
        {
            // 创建连接对象
            _connection = MqHelper.GetConnection();

            // 创建信道
            _channel = _connection.CreateModel();

            // 创建队列[其实可以不需要的，关键是队列是存在情况下]
            // _channel.QueueDeclare(simpleQueueName, false, false, false, null);

            _channel.BasicQos(0, 1, false);

            var messageReceiver = new MessageReceiver(_channel);

            _channel.BasicConsume(simpleQueueName, false, messageReceiver);            

        }
    }
}
