using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircuitBreaker.WebApi.Repository
{
    using CircuitBreaker.WebApi.Models;

    public class MovieRepository
    {
        private IList<Movie> movies;

        public MovieRepository()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.movies = new List<Movie>();

            this.movies.Add(this.CreateMovie(1,"The Dirty Dozen")); 
            this.movies.Add(this.CreateMovie(2,"Bambi")); 
            this.movies.Add(this.CreateMovie(3,"Dumbo")); 
            this.movies.Add(this.CreateMovie(4,"Forest Gump")); 
        }

        private Movie CreateMovie(int id, string name)
        {
            return new Movie(){Id = id, Name =  name};
        }

        public Movie RetrieveMovie(int Id)
        {
            return this.movies.SingleOrDefault(x => x.Id == Id);
        }
    }
}