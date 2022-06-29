using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L07.Models
{
    public class Subscriber
    {
        public int SubscriberId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool PublicProfile { get; set; }
        public bool AutoAcceptFriends { get; set; }
        public bool BroadcastPosts { get; set; }
    }
}

