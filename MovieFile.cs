using System;
using System.IO;
using System.Collections.Generic;
using NLog.Web;

namespace MediaLibrary{
    public class MovieFile{
        //MovieFile needs to hold all of the movies
        //It needs to read all of the movies in from the scrubbed movies file, and separate them into the appropriate categories, and create a new movie from it


        //Movies in MovieFile - init' once MovieFile is constructed
        List<Movie> movies;
        //Stream reader - init' once Movie File is constructed
        StreamReader sr;

        //Constructor - take in a file, try to read file, throw exception if cannot
        //Read all lines in file, and separate them into their appriopriate categories
        //Create new movie based on line
        //Go to next line

        //Return list of movies (so that they can be printed out)

        //Add to list of movies
    }
}