using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq_learn.Interfaces
{
    public interface IGenericMessageProducer
    {

        void SetExchange(string exchangeName, string type);
        void SetQueue(string QueueName);
        void Bind(string queueName, string exchangeName, string routingKey="");
        

        void SendMessageByExchange<T>(string queueName, T message);
        void SendMessageByRoutingKey<T>(string routingkey, T message);
        void SendMessage<T>(string exchangeName, string routingkey, T message);
        void SendMessage<T>(string exchangeName, string routingkey, Dictionary<string,object> headers,  T message);        
    }
}
