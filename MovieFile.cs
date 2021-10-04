using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using NLog.Web;

namespace MediaLibrary{
    public class MovieFile{
        //MovieFile needs to hold all of the movies
        //It needs to read all of the movies in from the scrubbed movies file, and separate them into the appropriate categories, and create a new movie from it


        //Movies in MovieFile - init' once MovieFile is constructed
        List<Movie> movies;
        //Stream reader - init' once Movie File is constructed
        StreamReader sr;

        //MovieFile constructor - takes in a file and a logger, and reads all the lines of the file, creating a list of movies from it
        public MovieFile(string file, NLog.Logger logger){
            logger.Info("Movie File Constructing...");
            //Init the movies list
            movies = new List<Movie>();
            //Try to init the stream reader
            try{
                sr = new StreamReader(file);
            } catch (Exception e){
                //If there's an error, log it
                logger.Error(e);
            }
            logger.Info($"File {file} opened");
            //Once the file is open, start reading from it
            //For each line in the file...
            logger.Info("Stream start");
            while (!sr.EndOfStream){
                //Take in the line
                string line = sr.ReadLine();
                //Get the index of the first quotation mark (if any)
                int index = line.IndexOf('"');
                //If there's a quotation mark in the line...
                if (index != -1){
                    //First, separate the ID
                    UInt64 id = UInt64.Parse(line.Substring(0,index-2));
                    //Cut the line off at the index (to remove the id and quotation mark)
                    line = line.Substring(index + 1);
                    //Find the index of the last quotation mark 
                    index = line.LastIndexOf('"');
                    //The title goes from the beginning of the line to the spot before the last quotation mark
                    string tempTitle = line.Substring(0,index - 1);
                    //Cut the line off right 1 space after the last quotation mark (to get rid of the comma)
                    line = line.Substring(index + 2);
                    //Split the line based on commas -- there are three more sections
                    string[] sections = line.Split(",");
                    //Create a list of genres based on the first section
                    List<string> tempGenres = sections[0].Split('|').ToList<string>();
                    //Create a new movie
                    Movie movie = new Movie{
                      mediaId = id,
                      title = tempTitle,
                      genres = tempGenres,
                      director = sections[1],
                      runningTime = TimeSpan.Parse(sections[2])
                    };
                    //Add the movie to the MovieFile list
                    movies.Add(movie);
                } else {
                    //If there is no quotation mark in the line...
                    //Separate based on commas
                    string[] sections = line.Split(',');
                    //Create the movie
                    Movie movie = new Movie{
                        //The ID is the first value in sections
                        mediaId = UInt64.Parse(sections[0]),
                        //The title is the second value in sections
                        title = sections[1],
                        //The genres are the third value in sections
                        genres = sections[2].Split('|').ToList<string>(),
                        //The director is the fourth value in sections
                        director = sections[3],
                        //The timestamp is the fifth value in sections
                        runningTime = TimeSpan.Parse(sections[4])
                    };
                    //Add the movie to the list
                    movies.Add(movie);
                }
            }
            logger.Info("Stream end");
            //Once all the lines are read, close the stream
            sr.Close();
            logger.Info("MovieFile constructed");
        }

        //Return list of movies (so that they can be printed out)
        public List<Movie> GetMovies(){
            return movies;
        }

        //Add to list of movies
        public void addMovie(Movie movie){
            movies.Add(movie);
        }
    }
}