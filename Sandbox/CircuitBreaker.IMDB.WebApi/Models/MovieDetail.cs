using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircuitBreaker.IMDB.WebApi.Models
{
    public class MovieDetail
    {
        public int Id
        {
            get;
            set;
        }

        public string Cast
        {
            get;
            set;
        }

        public string Producer
        {
            get;
            set;
        }
    }
}