using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using Recruiting.Domain.Infrastructure.Messaging;

namespace Recruiting.Messaging
{
    public class MsMqMessageStreamWriter : IMessageStreamWriter
    {

        private readonly MessageQueue _queue;
        public MsMqMessageStreamWriter(string path)
        {
            //if (!MessageQueue.Exists(path))
            //{
            //    _queue = MessageQueue.Create(path);
            //}
            //else
            //{
            //    _queue = new MessageQueue(path, QueueAccessMode.Send);
            //}
             _queue = new MessageQueue(path, QueueAccessMode.Send);
             
            _queue.Formatter = new JsonMessageFormatter(System.Text.Encoding.Default);          
        }

        public void DispatchAsync(IMessage message)
        {
            _queue.Send(message);
        }

        public void DispatchAsync(IEnumerable<IMessage> messages)
        {
            messages.ToList().ForEach(DispatchAsync);
        }
    }
}