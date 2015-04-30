using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CircuitBreaker.WebApi.Controllers
{
    using CircuitBreaker.WebApi.Models;
    using CircuitBreaker.WebApi.Repository;

    public class MovieController : ApiController
    {
        // GET: api/Movie
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Movie/5
        public Movie Get(int id)
        {
            MovieRepository movieRepository = new MovieRepository();

            return movieRepository.RetrieveMovie(id);
        }

        // POST: api/Movie
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Movie/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Movie/5
        public void Delete(int id)
        {
        }
    }
}
