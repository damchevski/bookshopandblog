using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity
{
    public class EmailMessage : Base
    {
        public string MailTo { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public Boolean Status { get; set; }
    }
}
