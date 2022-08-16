using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMq_Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_Consumer.Router.Fauout
{
    public class FanoutConsumer
    {
        public static void ConsumerMessage()
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
            channel.QueueBind("fanout_queue1", "fanout_exchange", "", null);
            channel.QueueBind("fanout_queue2", "fanout_exchange", "", null);
            channel.QueueBind("fanout_queue3", "fanout_exchange", "", null);



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
            channel.BasicConsume(queue: "fanout_queue1", autoAck: true, consumer);
            channel.BasicConsume(queue: "fanout_queue2", autoAck: true, consumer);
            channel.BasicConsume(queue: "fanout_queue3", autoAck: true, consumer);

            Console.ReadKey();
        }
    }
}
