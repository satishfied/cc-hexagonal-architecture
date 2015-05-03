using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircuitBreaker.WebApi.Repository
{
    using CircuitBreaker.WebApi.Models;

    public class IMDBService
    {
        public MovieDetailResponse RetrieveDetailsForMovie(int id)
        {
            return new MovieDetailResponse()
            {
                Id = 1,
                Cast = "Veel goei acteurs",
                Producer = "Steven spielerai"
            };
        }
    }
}