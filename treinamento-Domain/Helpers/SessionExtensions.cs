using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace treinamento_Domain.Helpers
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
             session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return string.IsNullOrWhiteSpace(value) || value == "[]" ? default : JsonConvert.DeserializeObject<T>(value);
        }

    }
}
