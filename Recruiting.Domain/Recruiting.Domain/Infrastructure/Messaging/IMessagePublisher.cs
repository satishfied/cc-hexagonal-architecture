using System.Collections.Generic;

namespace Recruiting.Domain.Infrastructure.Messaging
{
    public interface IMessagePublisher
    {
        IEnumerable<IMessage> Messages { get; }
    }
}