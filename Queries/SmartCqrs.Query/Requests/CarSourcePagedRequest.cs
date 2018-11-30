using SmartCqrs.Infrastructure.DapperEx;
using System;
using System.Collections.Generic;

namespace SmartCqrs.Query.Requests
{
    public class CarSourcePagedRequest : PagedRequest
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        public int? SeriesId { get; set; }

        /// <summary>
        /// 车规格Id
        /// </summary>
        public int? SizeId { get; set; }

        /// <summary>
        /// 最低售价
        /// </summary>
        public decimal? MinSalesPrice { get; set; }

        /// <summary>
        /// 最高售价
        /// </summary>
        public decimal? MaxSalesPrice { get; set; }

        /// <summary>
        /// 车辆销售区域Id
        /// </summary>
        public int? SalesAreaId { get; set; }

        /// <summary>
        /// 车辆销售省份Id
        /// </summary>
        public int? SalesProvinceId { get; set; }

        /// <summary>
        /// 车源用户Id
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 排序，支持多个字段排序，格式[{"key":"排序字段","value":#1：升序，2：降序#}]
        /// </summary>
        public List<KeyValuePair<string, SortType>> Sorts { get; set; }

        public string GenerateWhereSql()
        {
            string where = string.Empty;

            if (SeriesId.HasValue && SeriesId.Value > 0)
            {
                where += "and series_id = @SeriesId ";
            }

            if (SizeId.HasValue && SizeId.Value > 0)
            {
                where += "and size_id = @SizeId ";
            }

            if (MinSalesPrice.HasValue)
            {
                where += "and sales_price >= @MinSalesPrice ";
            }

            if (MaxSalesPrice.HasValue)
            {
                where += "and sales_price <= @MaxSalesPrice ";
            }

            if (SalesAreaId.HasValue && SalesAreaId.Value > 0)
            {
                where += "and sales_area_id = @SalesAreaId ";
            }

            if (SalesProvinceId.HasValue && SalesProvinceId.Value > 0)
            {
                where += "and sales_province_id = @SalesProvinceId ";
            }

            if(UserId.HasValue)
            {
                where += "and user_id = @UserId";
            }

            if(!string.IsNullOrWhiteSpace(Keyword))
            {
                where += "and (model_name like @Keyword or series_name like @Keyword )";
                Keyword = $"%{Keyword}%";
            }

            if(where.StartsWith("and"))
            {
                where = where.Substring(3);
            }

            return where;
        }

        public string GenerateOrderBySql()
        {
            string orderBy = string.Empty;

            if (Sorts != null && Sorts.Count > 0)
            {
                for (int i = 0; i < Sorts.Count; i++)
                {
                    var sort = Sorts[i];
                    if (i == 0)
                    {
                        orderBy += " order by ";
                    }

                    orderBy += $" {sort.Key} {sort.Value.ToString()} ";

                    if (i != Sorts.Count - 1)
                    {
                        orderBy += ",";
                    }
                }
            }

            return orderBy;
        }
    }
}
