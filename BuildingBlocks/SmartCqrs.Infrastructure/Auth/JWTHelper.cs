using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SmartCqrs.Infrastructure.Auth
{
    public class JWTHelper
    {
        public static List<string> AllowAnonymousPathList;
        public static string SecurityKey;
        /// <summary>
        /// 验证身份 验证签名的有效性,
        /// </summary>
        /// <param name="encodeJwt"></param>
        /// <param name="validatePayLoad">自定义各类验证； 是否包含那种申明，或者申明的值</param>
        /// 例如：payLoad["aud"]?.ToString() == "roberAuddience";
        /// 例如：验证是否过期 等
        /// <returns></returns>
        public static bool Validate(string encodeJwt, Func<Dictionary<string, object>, bool> validatePayLoad)
        {
            var success = true;
            var jwtArr = encodeJwt.Split('.');
            var header = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[0]));
            var payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[1]));
            
            var hs512 = new HMACSHA512(Encoding.UTF8.GetBytes(SecurityKey));
            //验证签名是否正确
            success = success && string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs512.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1])))));
            if (!success)
            {
                return success;//签名不正确直接返回
            }
            //其次验证是否在有效期内（也应该必须）
            var now = ToUnixEpochDate(DateTime.UtcNow);
            success = success && (now < long.Parse(payLoad["exp"].ToString()));

            //再其次 进行自定义的验证
            success = success && validatePayLoad(payLoad);

            return success;
        }

        //public bool IsAllowAnonymous(Microsoft.AspNetCore.Http.PathString path)
        //{
        //    return true;
        //}


        ///// <summary>
        ///// 创建jwttoken
        ///// </summary>
        ///// <param name="payLoad"></param>
        ///// <param name="header"></param>
        ///// <returns></returns>
        //public static string CreateToken(Dictionary<string, object> payLoad, int expiresMinute, Dictionary<string, object> header = null)
        //{
        //    var now = DateTime.UtcNow;
        //    var claims = new List<Claim>();
        //    foreach (var key in payLoad.Keys)
        //    {
        //        var tempClaim = new Claim(key, payLoad[key]?.ToString());
        //        claims.Add(tempClaim);
        //    }
        //    var jwt = new JwtSecurityToken(
        //        issuer: null,
        //        audience: null,
        //        claims: claims,
        //        notBefore: now,
        //        expires: now.Add(TimeSpan.FromMinutes(expiresMinute)),
        //        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey)), SecurityAlgorithms.HmacSha512));
        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        //    return encodedJwt;
        //}

        public static long ToUnixEpochDate(DateTime date) =>
                   (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


        public static ClientUser GetClientUser(string base64string)
        {
            return JsonConvert.DeserializeObject<ClientUser>(Base64UrlEncoder.Decode($"{base64string.Split('.')[1]}"));
        }
    }

    public class ClientUser
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Sub { get; set; }
        public long Created { get; set; }
        public string Nickname { get; set; }
        public long Exp { get; set; }

        /// <summary>
        /// 用户UserId
        /// </summary>
        public Guid UUID { get; set; }

        public string AccessToken { get; set; }
    }
}
