using System.Linq;

namespace Recruiting.Domain.Infrastructure.Messaging
{

    public interface IMessageStreamService
    {
        IMessageStream OpenReader(string streamName);
        IMessageStreamWriter OpenWriter(string streamName);
    }
}
