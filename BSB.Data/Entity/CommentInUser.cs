using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity
{
    public class CommentInUser : Base
    {
        public Guid CommentId { get; set; }
        public Comment Comment { get; set; }
        public string UserId { get; set; }
        public BSBUser User { get; set; }

    }
}
