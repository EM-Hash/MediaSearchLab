using System;
using NLog.Web;
using System.IO;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        //Method to add a movie to the MovieFile and the moviesscrubbed file
        public void addMovie(string file, MovieFile movieFile){
            //Generate an ID
            //Ask for, and save, the...
            //Title
            //Genre(s)
            //Director
            //Running Time

            //Open the streamwriter (APPEND)
            //Write the values to the filescrubber file
            //Close the streamwriter

            //Create a new movie

            //Add the movie to the MovieFile list
        }

        //Method to display all the movies in the MovieFile
        public void displayMovies(MovieFile movieFile){
            //Get the list of movies from movie file
            List<Movie> movies = movieFile.GetMovies();
            //For each value in the list...
            foreach (Movie m in movies){
                //Print the movie
                Console.WriteLine(m.Display());
            }
        }

        static void Main(string[] args)
        {
            logger.Info("Program started");

            //Create scrubbed file of movies
            // string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            // logger.Info(scrubbedFile);

            //Path to scrubbed file
            string file = "movies.scrubbed.csv";

            //Create movie file
            MovieFile movieFile = new MovieFile(scrubbedFile, logger);

            //Bool to determine if user is done or not
            bool cont = true;

            //Until the user is done...
            do{
                //Display the menu
                Console.WriteLine("Welcome to the Movie Library!");
                Console.WriteLine("Would you like to...");
                Console.WriteLine("[1] Add Movie");
                Console.WriteLine("[2] View all Movies");
                Console.WriteLine("[3] Quit");
                //Save the user's answer
                string ans = Console.ReadLine();
                //Go to switch statement
                switch(ans){
                    case "1":
                        //If 1, go to Add movie method
                        addMovie(file, movieFile);
                        break;
                    case "2":
                        //If 2, go to Display all movies method
                        displayMovies(movieFile);
                        break;
                    default:
                        //Else, quit program
                        break;
                }
            } while (cont);
            logger.Info("Program ended");
        }
    }
}