using System;
using System.Collections.Generic;
using System.Text;

namespace Movie_List
{
    class Movie
    {
        public enum Genre
        {
            drama,
            horror,
            animated,
            scifi,
        }

        public string Name { get; set; }
        public Genre genre { get; set; }

        public Movie(string name, Genre genre)
        {
            Name = name;
            this.genre = genre;
        }



    }
}
