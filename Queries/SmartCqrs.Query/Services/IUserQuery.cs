using SmartCqrs.Infrastructure.DapperEx;
using SmartCqrs.Query.Requests;
using SmartCqrs.Query.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCqrs.Query.Services
{
    public interface IUserQuery
    {
        Task<UserAssetVm> GetUserAssetAsync(Guid userId);

        Task<UserVm> GetUserAsync(Guid userId);

        Task<UserFullDetailVm> UserFullDetail(Guid userId);
    }
}
