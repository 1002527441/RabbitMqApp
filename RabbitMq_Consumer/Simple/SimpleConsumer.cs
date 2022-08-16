using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMq_Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_Consumer
{
    public class SimpleConsumer
    {

        public static void ConsumerMessage()
        {
            // 创建连接对象
            var connection = MqHelper.GetConnection();

            // 创建信道
            var channel = connection.CreateModel();

            // 创建队列[其实可以不需要的，关键是队列是存在情况下]
            channel.QueueDeclare("simple_queue", false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                // 获取消息
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var routingKey = eventArgs.RoutingKey;
                Console.WriteLine($"message is {message}, routingKey is {routingKey}");

            };

            //消费消息
            channel.BasicConsume(queue: "simple_queue", autoAck: true, consumer);

            Console.ReadKey();
        }
               
    }
}
