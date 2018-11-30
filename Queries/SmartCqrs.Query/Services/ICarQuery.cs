using SmartCqrs.Enumeration;
using SmartCqrs.Infrastructure.DapperEx;
using SmartCqrs.Query.Requests;
using SmartCqrs.Query.ViewModels;
using System.Threading.Tasks;

namespace SmartCqrs.Query.Services
{
    public interface ICarQuery
    {
        /// <summary>
        /// 获取车源分页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedData<CarListVm>> GetPagedDataAsync(CarSourcePagedRequest request);

        /// <summary>
        /// 获取车源明细
        /// </summary>
        /// <param name="carSourceId">车源Id</param>
        /// <returns></returns>
        Task<CarDetailVm> GetDetailAsync(int carSourceId);
    }
}
