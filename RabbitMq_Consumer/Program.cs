using RabbitMq_Consumer.Router.Direct;
using RabbitMq_Consumer.Router.Fauout;
using RabbitMq_Consumer.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // 简单队列消费
            //SimpleConsumer.ConsumerMessage();
            var simple2Consumer = new Simple2Consumer("abc");


            // 订阅消费消息
            // FanoutConsumer.ConsumerMessage();

            //路由消费消息
            //DirectConsumer.ConsumerMessage();

            Console.ReadKey();
        }
    }
}
