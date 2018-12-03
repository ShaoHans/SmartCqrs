using System;
using System.Text;
using System.Threading.Tasks;
using SmartCqrs.Enumeration;
using SmartCqrs.Infrastructure.DapperEx;
using SmartCqrs.Query.Requests;
using SmartCqrs.Query.ViewModels;

namespace SmartCqrs.Query.Services.Impls
{
    public class CarQuery : ICarQuery
    {
        private readonly DapperContext _dbContext;

        public CarQuery(DapperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CarDetailVm> GetDetailAsync(int carSourceId)
        {
            return await _dbContext.QueryFirstOrDefaultAsync<CarDetailVm>($"select id car_id, * from car where id = @Id " +
                $"and status != {1}", new { Id = carSourceId });
        }

        public async Task<PagedData<CarListVm>> GetPagedDataAsync(CarSourcePagedRequest request)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($@"select 
                        id car_id,brand_name,series_name,style_name,model_name,sales_price,
                        tag,image,status,user_id
                        from car where status != {1} ");
            string where = request.GenerateWhereSql();
            if(!string.IsNullOrWhiteSpace(where))
            {
                sql.AppendLine(" and " + where);
            }
            string orderBy = request.GenerateOrderBySql();
            if(string.IsNullOrWhiteSpace(orderBy))
            {
                sql.AppendLine("order by created_time desc");
            }
            else
            {
                sql.AppendLine(orderBy);
            }

            var carSources = await _dbContext.GetPagedDataAsync<CarListVm>(sql.ToString(), request, request.PageSize, request.PageNumber);
            return carSources;
        }
    }
}
