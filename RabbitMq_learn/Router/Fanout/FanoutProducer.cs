using RabbitMq_Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_learn.Router.Fanout
{
    public class FanoutProducer
    {
        public static void SendMessage()
        {
            // 创建连接对象
            var connection = MqHelper.GetConnection();

            // 创建信道
            var channel = connection.CreateModel();

            // 声明交换机
            channel.ExchangeDeclare("fanout_exchange", "fanout", false, false, null);

            // 创建队列
            channel.QueueDeclare("fanout_queue1", false, false, false, null);
            channel.QueueDeclare("fanout_queue2", false, false, false, null);
            channel.QueueDeclare("fanout_queue3", false, false, false, null);

            //把队列绑定到交换机
            channel.QueueBind("fanout_queue1", "fanout_exchange","", null);
            channel.QueueBind("fanout_queue2", "fanout_exchange", "", null);
            channel.QueueBind("fanout_queue3", "fanout_exchange",  "",  null);


            for (int i = 0; i < 10; i++)
            {
                string message = $" RabbitMQ  fanout message {i + 1}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("fanout_exchange", routingKey: "", mandatory: false, basicProperties: null, body);
                Console.WriteLine(message);

            }
        }
    }
}
