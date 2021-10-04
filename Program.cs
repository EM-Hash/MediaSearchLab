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
        
        //Method to display all the movies in the MovieFile
        
        static void Main(string[] args)
        {

            logger.Info("Program started");
            // string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            // logger.Info(scrubbedFile);

            //Until the user is done...
                //Display the menu
                //Save the user's answer
                //Go to switch statement
                    //If 1, go to Add movie method
                    //If 2, go to Display all movies method
                    //Else, quit program
            //Create movie file
            MovieFile tempFile = new MovieFile(scrubbedFile, logger);
            logger.Info("Program ended");
        }
    }
}