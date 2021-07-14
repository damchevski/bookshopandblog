using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity
{
    public class SmsMessage
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
    }
}
