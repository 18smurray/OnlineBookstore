using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Infrastructure
{
    public static class UrlExtensions
    {
        //Creating a url so users can return to browsing from the cart page
        //Set variable equal to the current request url; if there was a query, return
        //the query as part of the path, otherwise just convert the entire path to a string...?
        public static string PathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue ?
            $"{request.Path}{request.QueryString}" : request.Path.ToString();
    }
}
