using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity {
    public class Post : Base
    {
        public string Content { get; set; }
        public int Likes { get; set; }
        public string Topic { get; set; }

        public BSBUser ByUser { get; set; }
        public string ByUserId { get; set; }
        public virtual ICollection<CommentInPost> CommentsInPost { get; set; }

    }
}
