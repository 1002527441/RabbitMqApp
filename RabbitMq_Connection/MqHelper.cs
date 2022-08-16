using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMq_Connection
{
    public class MqHelper
    {

        /// <summary>
        ///  get the object of rabbit mq
        /// </summary>
        /// <returns></returns>
        public static  IConnection  GetConnection() 
        {
            // create the connection factory
            var connectionfactory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port=5672,
                VirtualHost ="/TestA",
                UserName="admin",
                Password="admin",                
            };

            return connectionfactory.CreateConnection();
        }






    }
}
