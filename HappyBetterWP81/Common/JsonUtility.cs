using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HappyBetterWP81.Common
{
    public class JsonUtility
    {
        public static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static T DeserializeObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
