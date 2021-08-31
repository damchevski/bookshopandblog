using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity {
    public class Comment : Base 
    {
        public string Content { get; set; }

        public BSBUser ByUser { get; set; }
        public CommentInPost CommentInPost { get; set; }
    }
}
