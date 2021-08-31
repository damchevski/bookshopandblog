using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity
{
    public class CommentInPost : Base
    {
        public Guid CommentId { get; set; }
        public Comment Comment { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
