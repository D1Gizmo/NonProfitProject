﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models.Extensions
{
    //session extension method allows complex objects to be stored in the session. 
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
             session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return (string.IsNullOrEmpty(value)) ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
