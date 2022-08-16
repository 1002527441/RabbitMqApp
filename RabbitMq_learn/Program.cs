using RabbitMq_learn.Router.Direct;
using RabbitMq_learn.Router.Fanout;
using RabbitMq_learn.Router.Topic;
using RabbitMq_learn.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_learn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region "simple_queue 简单队列模式1"        
            // SimpleProducer.SendMessage();
            #endregion

            #region "simple_queue 简单队列模式2"        
            //var simple2Producer = new Simple2Producer("abc");
            // simple2Producer.SendMessage("I am henry here");
            #endregion

            #region "simple_queue 简单队列模式3"        
            //var simple3Producer = new GenericMessageProducer();

            //var routingkey = "mq.simple2";
            //simple3Producer.SetQueue(routingkey);
            //simple3Producer.SendMessageByRoutingKey(routingkey, "I am henry here");
            #endregion

            #region "fanout_queue发布订阅模式1"        
            //FanoutProducer.SendMessage();
            #endregion

            #region "fanout_queue发布订阅模式2"       

            //var simple4Producer = new GenericMessageProducer();

            //var exchangeName = "mq.fanout.exchange";
            //simple4Producer.SetExchange(exchangeName, "fanout");
            //simple4Producer.SetQueue("mq.fanout.queue1");
            //simple4Producer.SetQueue("mq.fanout.queue2");
            //simple4Producer.SetQueue("mq.fanout.queue3");
            //simple4Producer.Bind( "mq.fanout.queue1", exchangeName);
            //simple4Producer.Bind("mq.fanout.queue2", exchangeName);
            //simple4Producer.Bind("mq.fanout.queue3", exchangeName);

            //simple4Producer.SendMessageByExchange(exchangeName, "I am henry here");

            #endregion

            #region "Direct_queue路由队列模式1"        
            //DirectProducer.SendMessage();
            #endregion
            #region "Direct_queue路由队列模式2"        
            var simple5Producer = new GenericMessageProducer();

            var exchangeName = "mq.direct.exchange";
            simple5Producer.SetExchange(exchangeName, "direct");
            simple5Producer.SetQueue("mq.direct.queue1");
            simple5Producer.SetQueue("mq.direct.queue2");
            simple5Producer.SetQueue("mq.direct.queue3");
            simple5Producer.Bind("mq.direct.queue1", exchangeName, "info");
            simple5Producer.Bind("mq.direct.queue2", exchangeName, "warn");
            simple5Producer.Bind("mq.direct.queue3", exchangeName, "error");

            simple5Producer.SendMessage(exchangeName, "info", "I am henry here");
            simple5Producer.SendMessage(exchangeName, "warn", "I am henry here1");
            simple5Producer.SendMessage(exchangeName, "error", "I am henry here2");
            simple5Producer.SendMessage(exchangeName, "error", "I am henry here3");

            #endregion

            #region "Topic_queue主题路由模式"        
            //TopicProducer.SendMessage();
            #endregion

            Console.ReadKey();
        }
    }
}
