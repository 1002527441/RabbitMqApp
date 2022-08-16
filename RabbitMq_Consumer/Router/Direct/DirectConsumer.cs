using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMq_Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_Consumer.Router.Direct
{
    public class DirectConsumer
    {
        public static void ConsumerMessage()
        {
            // 创建连接对象
            var connection = MqHelper.GetConnection();

            // 创建信道
            var channel = connection.CreateModel();

            // 声明交换机
            channel.ExchangeDeclare("direct_exchange", "direct", false, false, null);

            // 创建队列
            channel.QueueDeclare("direct_queue1", false, false, false, null);
            channel.QueueDeclare("direct_queue2", false, false, false, null);
            channel.QueueDeclare("direct_queue3", false, false, false, null);

            //把队列绑定到交换机
            channel.QueueBind("direct_queue1", "direct_exchange", "info", null);
            channel.QueueBind("direct_queue2", "direct_exchange", "warn", null);
            channel.QueueBind("direct_queue3", "direct_exchange", "error", null);



            var consumer = new EventingBasicConsumer(channel);


            //consumer.Received += HandlerMessage;

            consumer.Received += (model, ea) =>
            {
                // 获取消息
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                var routingKey = ea.RoutingKey;
                Console.WriteLine($"message is {message}, routingKey is {routingKey}");

            };

            //消费消息
            channel.BasicConsume(queue: "direct_queue1", autoAck: true, consumer);
            channel.BasicConsume(queue: "direct_queue2", autoAck: true, consumer);
            channel.BasicConsume(queue: "direct_queue3", autoAck: true, consumer);

            Console.ReadKey();
        }
        }
}
