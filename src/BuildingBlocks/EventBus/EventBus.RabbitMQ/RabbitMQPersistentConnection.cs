using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    public class RabbitMQPersistentConnection : IDisposable
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly int retryCount;
        private IConnection connection;
        private object lock_object=new object();
        private bool _disposed;

        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory,int retryCount=5)
        {
            this.connectionFactory = connectionFactory;
            this.retryCount = retryCount;
        }

        public bool IsConnected  => connection != null && connection.IsOpen; 

        public IModel CreateModel()
        {
            return connection.CreateModel();
        }
        public void Dispose()
        {
            _disposed = true;
             connection.Dispose();
        }

        public bool TryConnect()
        {
            lock (lock_object)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time)=>
                    {
                    }
                );

                policy.Execute(() =>
                {
                    connection=connectionFactory.CreateConnection();
                });
                if (IsConnected)
                {

                    connection.ConnectionShutdown += connection_ConnectionShutdown;
                    connection.CallbackException += connection_CallBackException;
                    connection.ConnectionBlocked += connection_ConnectionBlocked;

                    // log
                    return true;
                }
                return false;

            }
        }



        private void connection_ConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {

            if (_disposed) return; 

            TryConnect();
        }
      

        private void connection_CallBackException(object? sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            TryConnect();

        }

        private void connection_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }


    }
}
