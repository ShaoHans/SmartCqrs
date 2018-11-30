using SmartCqrs.Application.Commands;
using FluentValidation;
using System;
using SmartCqrs.Enumeration;

namespace SmartCqrs.Application.Validations
{
    public class PublishCarCommandValidator: AbstractValidator<PublishCarCommand>
    {
        public PublishCarCommandValidator()
        {
            RuleFor(r => r.CarId).GreaterThan(0).When(r => r.PublishMode == PublishMode.Edit || r.PublishMode == PublishMode.RePublish).WithMessage("编辑或重新发布车辆时，车辆id不能为空");
            RuleFor(r => r.CarId).LessThanOrEqualTo(0).When(r => r.PublishMode == PublishMode.New).WithMessage("发布车辆时，车辆id不能大于0");
            RuleFor(r => r.BrandName).NotEmpty().WithMessage("汽车品牌不能为空");
            RuleFor(r => r.SeriesName).NotEmpty().WithMessage("车系不能为空");
            RuleFor(r => r.StyleName).NotEmpty().WithMessage("车款式不能为空");
            RuleFor(r => r.ModelName).NotEmpty().WithMessage("车型名称不能为空");
            RuleFor(r => r.Colors).Must(c => { return c != null && c.Length > 0; }).WithMessage("车颜色不能为空");
            RuleFor(r => r.SizeId).GreaterThan(0).WithMessage("车规格不能为空");
            RuleFor(r => r.SizeName).NotEmpty().WithMessage("车规格不能为空");
            RuleFor(r => r.SalesPrice).GreaterThan(0).LessThan(10000).WithMessage("售价区间应在0~10000之间");
            RuleFor(r => r.GuidePrice).GreaterThan(0).LessThan(10000).WithMessage("指导价区间应在0~10000之间");
            RuleFor(r => r.Tag).NotEmpty().WithMessage("标签不能为空");
            RuleFor(r => r.Description).NotEmpty().WithMessage("车辆描述不能为空");
            RuleFor(r => r.Description).Must(c => { return !string.IsNullOrWhiteSpace(c) && c.Length <= 1000; }).WithMessage("车辆描述不能超过1000个字符");
            RuleFor(r => r.Images).Must(c => { return c != null && c.Length > 0; }).WithMessage("请上传车辆图片");
            RuleFor(r => r.StockQty).GreaterThan(0).WithMessage("库存数量必须大于0");
            RuleFor(r => r.LoginUserId).NotEqual(Guid.Empty).WithMessage("发布用户不能为空");
        }
    }
}
