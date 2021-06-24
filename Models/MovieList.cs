using System;
using System.Collections.Generic;

#nullable disable

namespace Movie_List.Models
{
    public partial class MovieList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int? RunTime { get; set; }
    }
}
