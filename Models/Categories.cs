using System;
using System.Collections.Generic;

namespace MyblogApp.Models
{
    public partial class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
