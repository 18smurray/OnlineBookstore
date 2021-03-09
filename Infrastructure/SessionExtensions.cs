using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

//Session string can only hold strings, ints, and bytes
//In order to store cart data, we have to convert it to a string (json) format

namespace OnlineBookstore.Infrastructure
{
    public static class SessionExtensions
    {
        //Method for setting cart data in session storage as a json text file
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        //Method for getting cart data, converting to original format
        public static T GetJson<T>(this ISession session, string key)
        {
            //Get data from session associated with key passed in
            var sessionData = session.GetString(key);

            //If there is no data in the session storage, return default
            //Otherwise Deserialize and return the data
            return sessionData == null ? default(T) :
                JsonSerializer.Deserialize<T>(sessionData);
        }
    }
}
