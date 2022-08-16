using Newtonsoft.Json;
using RabbitMq_Connection;
using RabbitMq_learn.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_learn.Simple
{
    public class SimpleProducer
    {
        public static void SendMessage()
        {
            // 创建连接对象
            var connection = MqHelper.GetConnection();

            // 创建信道
            var channel = connection.CreateModel();

            // 创建队列
            channel.QueueDeclare("simple_queue", false, false, false, null);

            for (int i = 0; i < 10; i++)
            {
                string message = $" Rabbit simple message {i + 1}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", "simple_queue", false, null, body);
                Console.WriteLine(message);                    
            }
        }        
    }
}
