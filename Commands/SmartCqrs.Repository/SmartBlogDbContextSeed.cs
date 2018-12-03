using Microsoft.EntityFrameworkCore;
using SmartCqrs.Domain.Models;
using SmartCqrs.Infrastructure.Log;
using System.Threading.Tasks;

namespace SmartCqrs.Repository
{
    public class SmartBlogDbContextSeed
    {
        public async Task SeedAsync(SmartBlogDbContext context, ILoggerManager loggerManager)
        {
            using (context)
            {
                context.Database.Migrate();

                //if (await context.SysConfigs.AnyAsync(s => s.ParamKey == "Default_Avatar_Url") == false)
                //{
                //    context.SysConfigs.Add(new SysConfig { ParamKey = "Default_Avatar_Url",
                //        ParamValue = "http://pic.qqtn.com/up/2018-3/15198831325527845.jpg",
                //        Remark = "默认头像Url" });
                //    loggerManager.Info("系统配置表添加key：Default_Avatar_Url");
                //}
                //await context.SaveChangesAsync();
            }
        }
    }
}
