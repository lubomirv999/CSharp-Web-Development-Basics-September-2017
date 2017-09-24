using System;
using System.Collections.Generic;

namespace ImportDatabaseExample
{
    public partial class Posts
    {
        public Posts()
        {
            Comments = new HashSet<Comments>();
        }

        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }

        public Users User { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }
}
