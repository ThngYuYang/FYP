using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L02.Models
{
    public class Greeting
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public Greeting(string to,
            string from,
            string title,
            string message)
        {
            To = to;
            From = from;
            Title = title;
            Message = message;
        }
    }
}
//20031509 Thng Yu Yang
