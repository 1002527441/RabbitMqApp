using RabbitMq_Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_learn.Router.Topic
{
    public class TopicProducer
    {
        public static void SendMessage()
        {
            // 创建连接对象
            var connection = MqHelper.GetConnection();

            // 创建信道
            var channel = connection.CreateModel();

            // 声明交换机
            channel.ExchangeDeclare("topic_exchange", "topic", false, false, null);

            // 创建队列
            channel.QueueDeclare("topic_queue1", false, false, false, null);
            channel.QueueDeclare("topic_queue2", false, false, false, null);
            channel.QueueDeclare("topic_queue3", false, false, false, null);

            //把队列绑定到交换机
            channel.QueueBind("topic_queue1", "topic_exchange", "user.insert", null);
            channel.QueueBind("topic_queue2", "topic_exchange", "user.update", null);

            //一个完整单词定义： aa bbc ccc -->一个单词, aaa.bbb.ccc 三个单词，有.号做为分隔
            channel.QueueBind("topic_queue3", "topic_exchange", "user.*", null);


            for (int i = 0; i < 10; i++)
            {
                string message = $" RabbitMQ  fanout message {i + 1}";
                var body = Encoding.UTF8.GetBytes(message);

                var r1 = 0;
                var b1 = Math.DivRem(i, 2, out r1);
 
                if (r1 == 0)
                {
                    channel.BasicPublish("topic_exchange", "user.update", false, null, body);
                }
                else
                {
                    channel.BasicPublish("topic_exchange", "user.insert", false, null, body);
                }


                Console.WriteLine(message);

            }
        }
    }
}
