using System;
using System.Collections.Generic;
using NLog.Web;
using System.IO;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        //Method to add a movie to the MovieFile and the moviesscrubbed file
        public static void addMovie(string file, MovieFile movieFile){
            //Generate an ID
            UInt64 id = movieFile.lastID() + 1;
            //Ask for, and save, the...
            //Title
            string tempTitle = getValue("title");
            //Genre(s)
            List<string> tempGenres = new List<string>();
            //While the user is still entering genres...
            Boolean genCont = true;
            do{
                //Prompt/save the value
                tempGenres.Add(getValue("next genre"));
                //Ask if the user wants to continue
                Console.WriteLine("Add another genre? [Y/N]: ");
                string ans = Console.ReadLine().ToLower();
                //If so, continue; if not, break out
                if (ans == "y"){
                    continue;
                } else {
                    genCont = false;
                }
            } while (genCont);
            //Director
            string tempDir = getValue("director");
            //Running Time
            TimeSpan duration;
            while(!TimeSpan.TryParse(getValue("duration [hh:mm:ss]"),out duration)){
                Console.WriteLine("That is not a valid time value.");
            }
            //Open the streamwriter (APPEND)
            StreamWriter sw = new StreamWriter(file, true);
            //Write the values to the filescrubber file
            //If there's a comma or quotation mark in the title...
            if (tempTitle.Contains('"') || tempTitle.Contains(",")){
                //Alter the title to be encircled in quotation marks
                tempTitle = "\"" + tempTitle + "\"";
            }
            sw.WriteLine($"{id},{tempTitle},{string.Join("|",tempGenres.ToArray())},{tempDir},{duration}");
            //Close the streamwriter
            sw.Close();
            //Create a new movie
            Movie movie = new Movie{
                mediaId = id,
                title = tempTitle,
                genres = tempGenres,
                director = tempDir,
                runningTime = duration,
            };
            //Add the movie to the MovieFile list
            movieFile.addMovie(movie);
        }

        //Method to display all the movies in the MovieFile
        public static void displayMovies(MovieFile movieFile){
            //Get the list of movies from movie file
            List<Movie> movies = movieFile.GetMovies();
            //For each value in the list...
            foreach (Movie m in movies){
                //Print the movie
                Console.WriteLine(m.Display());
            }
        }

        //Method to get and return a string value
        public static string getValue(string valueName){
            Console.WriteLine($"Please enter the {valueName}: ");
            return Console.ReadLine();
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
            MovieFile movieFile = new MovieFile(file, logger);

            //Bool to determine if user is done or not
            bool cont = true;

            //Until the user is done...
            do{
                //Display the menu
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome to the Movie Library!");
                Console.WriteLine("Would you like to...");
                Console.WriteLine("[1] Add Movie");
                Console.WriteLine("[2] View all Movies");
                Console.WriteLine("[3] Search Movie");
                Console.WriteLine("[4] Quit");
                //Save the user's answer
                Console.ForegroundColor = ConsoleColor.White;
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
                    case "3":
                        //If 3, go to Search movies method
                    default:
                        //Else, quit program
                        cont = false;
                        break;
                }
            } while (cont);
            logger.Info("Program ended");
        }
    }
}