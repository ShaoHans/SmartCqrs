using Newtonsoft.Json;
using System;

namespace SmartCqrs.Infrastructure.Results
{
    public class CommandResult<T> 
    {
        /// <summary>
        /// 操作状态码
        /// </summary>
        public ResultCode Code { get; }

        /// <summary>
        /// 操作返回消息
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; }

        [JsonIgnore]
        public bool Success { get { return Code == ResultCode.SUCCESS; } }

        public CommandResult(ResultCode code = ResultCode.SUCCESS, string message = "", T data = default)
        {
            Code = code;
            Message = string.IsNullOrWhiteSpace(message) ? code.ToDescription() : message;
            Data = data;
        }
    }

    public class CommandResult : CommandResult<string>
    {
        public CommandResult(ResultCode code = ResultCode.SUCCESS, string message = "", string data = null) : base(code, message, data)
        {
        }
    }
}
