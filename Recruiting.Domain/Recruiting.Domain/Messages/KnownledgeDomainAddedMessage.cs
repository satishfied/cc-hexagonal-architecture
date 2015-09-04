using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.Domain.Messages
{
    class KnownledgeDomainAddedMessage
    {
        public string ScreeningId { get; set; }
        public string KnownledgeDomainId { get; set; }
    }
}
