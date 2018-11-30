using SmartCqrs.Application.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace SmartCqrs.Application.Commands
{
    public class UpdateUserInfoCommand : BaseCommand
    {
        /// <summary>
        /// 被更新的用户信息
        /// </summary>
        public JsonPatchDocument<UpdateUserInfoDto> UserPatch { get; set; }
    }
}
