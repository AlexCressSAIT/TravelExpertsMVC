using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelExperts.Models
{
    /*
     * Provides serialization/deserialization extensions for ISession
     * Author: code from class
     * February 2022
     */
    public static class SessionSerializationExtensions
    {
        /// <summary>
        /// Serializes an object and saves it in the session
        /// </summary>
        /// <typeparam name="T">The type of data being serialized</typeparam>
        /// <param name="session">We are extending the ISession interface</param>
        /// <param name="key">The key to reference the object</param>
        /// <param name="value">The data to be serialized</param>
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Deserializes an object and returns it
        /// </summary>
        /// <typeparam name="T">The type of data being deserialized</typeparam>
        /// <param name="session">We are extending the ISession interface</param>
        /// <param name="key">The key to reference the data being deserialized</param>
        /// <returns>Deserialized data of type T</returns>
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return (string.IsNullOrEmpty(value)
                ? default(T)
                : JsonConvert.DeserializeObject<T>(value)
                );
        }
    }
}
