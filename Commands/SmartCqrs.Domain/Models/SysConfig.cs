using SmartCqrs.Domain.SeedWork;

namespace SmartCqrs.Domain.Models
{
    /// <summary>
    /// 系统设置表（由开发人员维护）
    /// </summary>
    public class SysConfig : Entity
    {
        public string ParamKey { get; set; }

        public string ParamValue { get; set; }

        public int Status { get; set; }

        public string Remark { get; set; }
    }
}
