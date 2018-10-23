using System;
using System.Collections.Generic;

namespace MyblogApp.Models
{
    public partial class Recipies
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
    }
}
