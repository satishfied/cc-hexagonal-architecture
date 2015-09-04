using System;
using System.Collections.Generic;
using System.Messaging;
using Recruiting.Domain.Infrastructure.Messaging;

namespace Recruiting.Messaging
{
    public class MsMqMessageStream : IMessageStream, IDisposable
    {
        private readonly string _path;
        private readonly List<IObserver<IMessage>> _observers = new List<IObserver<IMessage>>();

        private   MessageQueue _queue;
        private bool _isDisposed;

        public MsMqMessageStream(string path)
        {
            _path = path;
        }

        protected void ListenReceive()
        {            
             _queue.BeginReceive(); 
        }

        private void OnReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var message = _queue.EndReceive(e.AsyncResult);
                if (message != null)
                {
                    DispatchLocal(message.Body as IMessage);
                }              
            }
            catch (MessageQueueException)
            {
                // Handle sources of MessageQueueException.
            }
            finally
            {
                ListenReceive();
            }
        }
         
        public IDisposable Subscribe(IObserver<IMessage> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public void Open()
        {
            if (!MessageQueue.Exists(_path))
            {
                _queue = MessageQueue.Create(_path);
            }
            else
            {
                _queue = new MessageQueue(_path, QueueAccessMode.Receive);
            }
            _queue.MulticastAddress = "234.1.1.1:8001";
            _queue.SetPermissions("ANONYMOUS LOGON", MessageQueueAccessRights.WriteMessage);
            _queue.ReceiveCompleted += OnReceiveCompleted;            
            _queue.Formatter = new JsonMessageFormatter(System.Text.Encoding.Default);
            ListenReceive();
        }

        private void DispatchLocal(IMessage ev)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(ev);
            }
        }

        public void Dispose()
        {
            try
            {
                _observers.ForEach(x => x.OnCompleted());
            }
            finally 
            {                
                _isDisposed = true;
            }           
        }

        private class Unsubscriber : IDisposable
        {
            private readonly IObserver<IMessage> _observer;
            private readonly List<IObserver<IMessage>> _observers;

            public Unsubscriber(List<IObserver<IMessage>> observers, IObserver<IMessage> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            #region IDisposable Members

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }

            #endregion
        }

    }
}