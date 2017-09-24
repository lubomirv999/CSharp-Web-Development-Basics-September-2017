using System;
using System.Collections.Generic;

namespace ImportDatabaseExample
{
    public partial class Users
    {
        public Users()
        {
            Comments = new HashSet<Comments>();
            Posts = new HashSet<Posts>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }

        public ICollection<Comments> Comments { get; set; }
        public ICollection<Posts> Posts { get; set; }
    }
}
