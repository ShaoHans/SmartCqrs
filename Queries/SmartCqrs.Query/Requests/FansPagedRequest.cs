using Newtonsoft.Json;
using System;

namespace SmartCqrs.Query.Requests
{
    public class FansPagedRequest : PagedRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
