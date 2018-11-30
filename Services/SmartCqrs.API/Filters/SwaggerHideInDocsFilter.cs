using SmartCqrs.Application.Commands;
using SmartCqrs.Infrastructure.Attributes;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

namespace SmartCqrs.API.Filters
{
    public class SwaggerHideInDocsFilter : IDocumentFilter
    {
        /*
         在API Controller的Action中，通过[FromForm]标注的参数的类型中，如果有些属性不想暴露在swagger文档中，可以通过以下方式过滤掉相关属性
        */


        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var api in context.ApiDescriptions.Where(a => !a.HttpMethod.Equals("get", StringComparison.OrdinalIgnoreCase)))
            {
                if (api.ActionDescriptor.Parameters == null || api.ActionDescriptor.Parameters.Count <= 0)
                {
                    continue;
                }

                foreach (var para in api.ActionDescriptor.Parameters.Where(p => typeof(Command).IsAssignableFrom(p.ParameterType)))
                {
                    var properties = para.ParameterType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Where(p => p.GetCustomAttributes(typeof(HideInDocsAttribute), true)?.Any() == true)
                                .Select(p => new { p.Name });
                    if (properties.Count() > 0)
                    {
                        var docParas = swaggerDoc.Paths.Where(p => p.Key.Equals($"/{api.RelativePath}", StringComparison.OrdinalIgnoreCase))
                            .Select(p => p.Value).FirstOrDefault();
                        if(docParas == null || docParas.Post == null || docParas.Post.Parameters == null)
                        {
                            continue;
                        }
                        foreach (var prop in properties)
                        {
                            var docPara = docParas.Post.Parameters.FirstOrDefault(dp => dp.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));
                            if(docPara != null)
                            {
                                docParas.Post.Parameters.Remove(docPara);
                            }
                        }
                    }
                }
            }
        }
    }
}
