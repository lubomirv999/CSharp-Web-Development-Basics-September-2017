using System;
using System.Collections.Generic;

namespace ImportDatabaseExample
{
    public partial class Comments
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }

        public Posts Post { get; set; }
        public Users User { get; set; }
    }
}
