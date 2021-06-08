using System;
using System.Collections.Generic;
using System.Linq;
using static Movie_List.Movie;

namespace Movie_List
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Movie> AllMovies = new List<Movie>();

            AllMovies.Add(new Movie("The Ring", Genre.horror));
            AllMovies.Add(new Movie("Frozen", Genre.animated));
            AllMovies.Add(new Movie("Transformers", Genre.scifi));
            AllMovies.Add(new Movie("The Cabin in the Woods", Genre.horror));
            AllMovies.Add(new Movie("Moana", Genre.animated));
            AllMovies.Add(new Movie("Moulin Rouge", Genre.drama));
            AllMovies.Add(new Movie("Cloudy with a Chance of Meatballs", Genre.animated));
            AllMovies.Add(new Movie("Avengers: Endgame", Genre.scifi));
            AllMovies.Add(new Movie("Star Wars: A New Hope", Genre.scifi));
            AllMovies.Add(new Movie("A Walk to Remember", Genre.drama));


            //Puts list in alphabetical order
            var moviesInOrder = AllMovies.OrderBy(s => s.Name);

            Console.WriteLine($"It's a movie list!");
            Console.WriteLine("Please enter the index number or genre name to select a category:");
            Genre[] acceptedGenres = (Genre[])Enum.GetValues(typeof(Genre));

            bool goOn = true;
            while (goOn == true)
            {
                try
                {
                    for (int z = 0; z < acceptedGenres.Count(); z++)
                    {
                        //Writes out the index and the genre at that index

                        Genre y = acceptedGenres[z];
                        Console.WriteLine($"{z} : {y}");
                    }
                    string input = Console.ReadLine().ToLower();
                    Genre inputGenre = (Genre)Enum.Parse(typeof(Genre), input);

                    foreach (Movie m in moviesInOrder)
                    {
                        if (m.genre == inputGenre)
                        {
                            //Writes out movie names that have the same genre as the input, pulling from the alphabetized list.
                            Console.WriteLine(m.Name);
                        }
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("That is not a valid input.  Please enter the genre or index.");
                    continue;
                }
                goOn = GetContinue();
            }
        }

        public static bool GetContinue()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Would you like to continue? y/n");
                string answer = Console.ReadLine();
                if (answer.ToLower().StartsWith("y"))
                {
                    return true;
                }
                else if (answer.ToLower().StartsWith("n"))
                {
                    Console.WriteLine("Have a great day!");
                    return false;
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Would you like to continue? y/n");
            }
            return GetContinue();
    }
}
}
