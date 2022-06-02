using Newtonsoft.Json;

namespace COMN.Extensions
{
    public static class ObjectExtensions
    {
        public static string Serialize(this object obj)
        {
            if (obj == null) return null;
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(this string str) where T : class
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}