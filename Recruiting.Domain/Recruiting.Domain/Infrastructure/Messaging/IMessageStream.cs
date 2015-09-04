using System;

namespace Recruiting.Domain.Infrastructure.Messaging
{
    public interface IMessageStream:IObservable<IMessage>
    {
        void Open();
    }
}