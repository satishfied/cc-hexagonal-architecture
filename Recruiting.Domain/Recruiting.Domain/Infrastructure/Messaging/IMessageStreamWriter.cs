using System.Collections.Generic;

namespace Recruiting.Domain.Infrastructure.Messaging
{
    public interface IMessageStreamWriter
    {
        void DispatchAsync(IMessage message);
        void DispatchAsync(IEnumerable<IMessage> messages);
    }
}