using System;
using System.Collections.Generic;

namespace MyblogApp.Models
{
    public partial class Posts
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
