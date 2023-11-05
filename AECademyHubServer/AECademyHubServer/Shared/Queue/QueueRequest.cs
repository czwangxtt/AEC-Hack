using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AECademyHubServer.Shared.Queue
{
    public class QueueRequest
    {
        [Key]
        public Guid UserGuid { get; set; }
        public Guid ObjectGuid { get; set; }
    }
}
