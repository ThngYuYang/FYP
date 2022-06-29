using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L07.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public int SubscriberId { get; set; }
        public int ProviderId { get; set; }
        public DateTime DateSubscribed { get; set; }
    }
}
