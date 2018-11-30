using SmartCqrs.Infrastructure.DapperEx;
using System;
using System.Collections.Generic;

namespace SmartCqrs.Query.Requests
{
    public class FindCarPagedRequest : PagedRequest
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
        /// 最低期望价
        /// </summary>
        public decimal? MinExpectationPrice { get; set; }

        /// <summary>
        /// 最高期望价
        /// </summary>
        public decimal? MaxExpectationPrice { get; set; }

        /// <summary>
        /// 寻车区域Id
        /// </summary>
        public int? FindAreaId { get; set; }

        /// <summary>
        /// 寻车省份Id
        /// </summary>
        public int? FindProvinceId { get; set; }

        /// <summary>
        /// 寻车用户Id
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

            if (MinExpectationPrice.HasValue)
            {
                where += "and expectation_price >= @MinExpectationPrice ";
            }

            if (MaxExpectationPrice.HasValue)
            {
                where += "and expectation_price <= @MaxExpectationPrice ";
            }

            if (FindAreaId.HasValue && FindAreaId.Value > 0)
            {
                where += "and find_area_id = @FindAreaId ";
            }

            if (FindProvinceId.HasValue && FindProvinceId.Value > 0)
            {
                where += "and find_province_id = @FindProvinceId ";
            }

            if (UserId.HasValue)
            {
                where += "and user_id = @UserId";
            }

            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                where += "and (model_name like @Keyword or series_name like @Keyword )";
                Keyword = $"%{Keyword}%";
            }

            if (where.StartsWith("and"))
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
