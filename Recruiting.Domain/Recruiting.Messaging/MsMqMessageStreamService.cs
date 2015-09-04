using System.Collections.Generic;
using Recruiting.Domain.Infrastructure.Messaging;

namespace Recruiting.Messaging
{
    public class MsMqMessageStreamService : IMessageStreamService
    {
        
        public IMessageStream OpenReader(string streamName)
        {
            return new MsMqMessageStream(streamName);
        }

        public IMessageStreamWriter OpenWriter(string streamName)
        {
            return new MsMqMessageStreamWriter(streamName);
        }
    }
}