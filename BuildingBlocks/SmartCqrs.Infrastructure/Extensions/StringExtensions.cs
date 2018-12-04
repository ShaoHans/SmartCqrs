using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        public static T DeserializeObject<T>(this string input)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return default;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(input);
            }
        }

        /// <summary>
        /// 对字符串进行MD5哈希
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5Hash(this string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").ToLower();
            }
        }

        public static Guid ToGuid(this string input)
        {
            if(!Guid.TryParse(input,out Guid result))
            {
                throw new InvalidCastException($"{input}转换为Guid类型失败");
            }
            return result;
        }

    }
}
