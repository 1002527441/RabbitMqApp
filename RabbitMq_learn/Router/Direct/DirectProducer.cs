using RabbitMq_Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_learn.Router.Direct
{
    public class DirectProducer
    {
        public static void SendMessage()
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


            for (int i = 0; i < 10; i++)
            {
                string message = $" RabbitMQ  fanout message {i + 1}";
                var body = Encoding.UTF8.GetBytes(message);

                var r1 = 0;
                var b1 = Math.DivRem(i , 2, out r1);

               if (i == 0){
                    channel.BasicPublish("direct_exchange", "error", false, null, body);
                }

                if (r1 == 0)
                {
                    channel.BasicPublish("direct_exchange", "info", false, null, body);
                }
                else
                {
                    channel.BasicPublish("direct_exchange", "warn", false, null, body);
                }


                Console.WriteLine(message);

            }
        }
    }
}
