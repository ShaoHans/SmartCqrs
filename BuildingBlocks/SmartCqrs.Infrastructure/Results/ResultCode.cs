using System.ComponentModel;

namespace SmartCqrs.Infrastructure.Results
{
    public enum ResultCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        SUCCESS = 200,

        /// <summary>
        /// 操作失败
        /// </summary>
        [Description("操作失败")]
        FAIL = 400,

        /// <summary>
        /// 未认证（签名错误）
        /// </summary>
        [Description("未认证（签名错误）")]
        UNAUTHORIZED = 401,

        /// <summary>
        /// 已冻结
        /// </summary>
        NOTVALID = 402,

        /// <summary>
        /// 访问被拒绝(没有权限)
        /// </summary>
        [Description("访问被拒绝(没有权限)")]
        ACCESS_IS_DENIED = 403,

        /// <summary>
        /// 数据不存在
        /// </summary>
        [Description("数据不存在")]
        NOT_FOUND = 404,

        /// <summary>
        /// 服务器内部错误
        /// </summary>
        [Description("服务器内部错误")]
        INTERNAL_SERVER_ERROR = 500,

        /// <summary>
        /// 积分不足
        /// </summary>
        [Description("积分不足")]
        POINT_NOT_ENOUGH = 1000,

        /// <summary>
        /// 每日每车一次刷新机会已使用
        /// </summary>
        [Description("每日每车一次刷新机会已使用")]
        EveryDay_RefreshCar_Opportunity_Used = 1001,

        /// <summary>
        /// 尚未通过企业认证不能发布车辆
        /// </summary>
        [Description("尚未通过企业认证不能发布车辆")]
        Enterprise_Audited_Not_Pass = 1002,
    }
}
