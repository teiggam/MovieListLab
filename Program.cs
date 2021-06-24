using Movie_List.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Movie_List
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"It's a movie list!\n");

            bool goOn = true;
            while (goOn == true)
            {
                Console.WriteLine("How would you like to proceed?");
                Console.WriteLine("Search, Edit, Add, Delete");
                string intent = Console.ReadLine().ToLower();
                GetIntent(intent);

                goOn = GetContinue("Would you like to continue?");
            }
            Console.Clear();
            Console.WriteLine("\nHave a great day!");
        }


        public static bool GetContinue(string message)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine(message);
                string answer = Console.ReadLine();
                if (answer.ToLower().StartsWith("y"))
                {
                    Console.Clear();
                    return true;
                }
                else if (answer.ToLower().StartsWith("n"))
                {
                    return false;
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("THat is not a valid response.");
            }
            return GetContinue("Please resond... y/n.");
        }

        public static void PrintList(List<MovieList> movies)
        {
            Console.WriteLine();
            if (movies.Count == 0)
            {
                Console.WriteLine("There are no movies found within those parameters.");
            }
            foreach (MovieList m in movies)
            {
                Console.WriteLine($"Movie Title\t{m.Title}");
                Console.WriteLine($"Movie Genre:\t{m.Genre}");
                Console.WriteLine($"Runtime:\t{m.RunTime}\n");
            }
        }
        public static void PrintIDTitleList(List<MovieList> movies)
        {
            foreach (MovieList m in movies)
            {
                Console.WriteLine($"{m.Id}: {m.Title}");
            }
        }

        public static void GetIntent(string input)
        {
            Console.Clear();
            MoviesContext db = new MoviesContext();
            List<MovieList> movies = db.MovieLists.ToList();

            if (input == "search")
            {
                bool goOn = true;
                while (goOn == true)
                {
                    Console.WriteLine("How would you like to search the movies list?");
                    Console.WriteLine("Title, genre, all");
                    string userInput = Console.ReadLine().ToLower();
                    Console.WriteLine();
                    GetSearchIntent(userInput);

                    goOn = GetContinue("Would you like to search again?");
                }
            }
            else if (input == "add")
            {
                bool goOn = true;
                while (goOn == true)
                {
                    Console.WriteLine("Please enter a movie title:");
                    string movieTitle = Console.ReadLine();
                    Console.WriteLine("Please enter the genre:");
                    string movieGenre = Console.ReadLine();
                    Console.WriteLine("Please enter the runtime in minutes:");
                    int movieRuntime = int.Parse(Console.ReadLine());
                    MovieList newMov = new MovieList() { Title = movieTitle, Genre = movieGenre, RunTime = movieRuntime };
                    AddMovie(newMov, db);
                    Console.WriteLine("You have successfully added the following movie to the database.");
                    List<MovieList> addedMovie = SearchMovieByTitle(movieTitle, db);
                    PrintList(addedMovie);

                    goOn = GetContinue("Would you like to add another movie to the list?");
                }
            }
            else if (input == "delete")
            {
                bool goOn = true;
                while (goOn == true)
                {
                    Console.WriteLine("Please enter the number of the movie you'd like to remove from the list:");
                    PrintIDTitleList(movies);
                    int delete = int.Parse(Console.ReadLine());
                    DeleteMovie(delete, db);
                    Console.WriteLine("The movie has been removed.");
                    List<MovieList> newMovies = db.MovieLists.ToList();
                    PrintIDTitleList(newMovies);

                    goOn = GetContinue("Would you like to remove another movie?");

                }

            }
            else if (input == "edit")
            {
                bool goOn = true;
                while (goOn == true)
                {
                    Console.WriteLine("You can edit the title of any movie.");
                    Console.WriteLine("Please enter the number of the movie you'd like to edit.");
                    PrintIDTitleList(movies);
                    Console.WriteLine();
                    int edit = int.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter the title you'd like to change it to:");
                    string newTitle = Console.ReadLine();
                    UpdateMovieTitle(edit, db, newTitle);
                    Console.WriteLine("The title has been updated.");

                    goOn = GetContinue("Would you like to edit another movie title?");
                }
            }

        }

        public static void GetSearchIntent(string input)
        {
            MoviesContext db = new MoviesContext();
            List<MovieList> movies = db.MovieLists.ToList();

            if (input == "genre")
            {
                Console.WriteLine("What genre would you like to search?");
                string inputGenre = Console.ReadLine().ToLower();
                Console.WriteLine();
                List<MovieList> results = SearchMovieByGenre(inputGenre, db);
                PrintList(results);
            }
            else if (input == "title")
            {
                Console.WriteLine("Please enter a keyword to search for a title.");
                string userInput = Console.ReadLine().ToLower();
                List<MovieList> results = SearchMovieByTitle(userInput, db);
                PrintList(results);

            }

            else if (input == "all")
            {
                PrintList(movies);
            }
            else
            {
                Console.WriteLine("That is not a valid input.");
                Console.WriteLine("Title, genre, all"); ;


            }
        }

        //Decided I didn't want people to search movies by ID number,
        //so it is here, and functions, but is not in use.
        //public static MovieList SearchMovieListByID(int id, MoviesContext db)
        //{
        //    try
        //    {
        //        MovieList m = db.MovieLists.Find(id);
        //        return m;
        //    }
        //    catch (NullReferenceException)
        //    {
        //        Console.WriteLine($"No movie data found at {id} index.");
        //        return null;
        //    }
        //}

        public static List<MovieList> SearchMovieByTitle(string title, MoviesContext db)
        {
            List<MovieList> results = db.MovieLists.Where(x => x.Title.Contains(title.ToLower())).ToList();

            return results;

        }

        public static List<MovieList> SearchMovieByGenre(string title, MoviesContext db)
        {
            List<MovieList> results = db.MovieLists.Where(x => x.Genre == title).ToList();

            return results;

        }


        public static void AddMovie(MovieList newMovie, MoviesContext db)
        {

            db.MovieLists.Add(newMovie);


            db.SaveChanges();

        }

        public static void UpdateMovieTitle(int id, MoviesContext db, string title)
        {

            MovieList m = db.MovieLists.Find(id);
            m.Title = title;
            db.MovieLists.Update(m);
            db.SaveChanges();

        }

        public static void DeleteMovie(int title, MoviesContext db)
        {
            //Grab the student
            MovieList m = db.MovieLists.Find(title);

            db.MovieLists.Remove(m);
            db.SaveChanges();

        }





    }
}
