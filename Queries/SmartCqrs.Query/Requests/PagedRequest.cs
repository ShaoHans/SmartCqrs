namespace SmartCqrs.Query.Requests
{
    public class PagedRequest
    {
        /// <summary>
        /// 每页显示条数（默认10条）
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 当前页码（默认第1页）
        /// </summary>
        public int PageNumber { get; set; } = 1;
    }
}
