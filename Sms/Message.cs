using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sms
{
    class Message
    {
        public string[] registration_ids { get; set; }
        public Notification notification { get; set; }
        public object data { get; set; }
    }
}
