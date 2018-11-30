using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartCqrs.Enumeration;
using SmartCqrs.Infrastructure.DapperEx;
using SmartCqrs.Query.Requests;
using SmartCqrs.Query.ViewModels;

namespace SmartCqrs.Query.Services.Impls
{
    public class UserQuery : IUserQuery
    {
        private readonly DapperContext _dbContext;

        public UserQuery(DapperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserFullDetailVm> UserFullDetail(Guid userId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ui.user_id");
            sql.AppendLine("      ,ui.mobile");
            sql.AppendLine("      ,ui.nick_name");
            sql.AppendLine("      ,ui.avatar");
            sql.AppendLine("      ,ui.province_name");
            sql.AppendLine("      ,ui.city_name");
            sql.AppendLine("      ,coalesce(ua.selling_car_count,0) selling_car_count");
            sql.AppendLine("      ,coalesce(ua.collect_car_count) collect_car_count");
            sql.AppendLine("      ,coalesce(ua.order_count) buy_order_count");
            sql.AppendLine("from userinfo ui");
            sql.AppendLine("left join user_asset ua");
            sql.AppendLine("    on ui.user_id = ua.user_id");
            sql.AppendLine("where ui.user_id = @UserId");
            
            return await _dbContext.QueryFirstOrDefaultAsync<UserFullDetailVm>(sql.ToString(), new { UserId = userId });
        }

        public async Task<UserAssetVm> GetUserAssetAsync(Guid userId)
        {
            return await _dbContext.QueryFirstOrDefaultAsync<UserAssetVm>($"select * from user_asset where user_id = @UserId", new { UserId = userId });
        }

        public async Task<UserVm> GetUserAsync(Guid userId)
        {
            return await _dbContext.QueryFirstOrDefaultAsync<UserVm>($"select * from userinfo where user_id = @UserId", new { UserId = userId });
        }
    }
}
