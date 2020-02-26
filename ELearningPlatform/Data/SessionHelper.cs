using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ELearningPlatform.Data
{
    public static class SessionHelper
    {
        public const string SessionKeyUser = "_User";

        public static void Set<T>(ISession session, string key, T value) => session.SetString(key, JsonConvert.SerializeObject(value));

        public static T Get<T>(ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
