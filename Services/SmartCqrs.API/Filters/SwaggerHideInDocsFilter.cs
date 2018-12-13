using SmartCqrs.Application.Commands;
using SmartCqrs.Infrastructure.Attributes;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SmartCqrs.API.Filters
{
    public class SwaggerHideInDocsFilter : IDocumentFilter
    {
        /*
         在API Controller的Action中，如果有些参数类型的属性不想暴露在swagger文档中，可以通过以下方式过滤掉相关属性
        */


        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var api in context.ApiDescriptions)
            {
                if (api.ActionDescriptor.Parameters == null || api.ActionDescriptor.Parameters.Count <= 0)
                {
                    continue;
                }

                foreach (var para in api.ActionDescriptor.Parameters)
                {
                    var properties = para.ParameterType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Where(p => p.GetCustomAttributes(typeof(HideInDocsAttribute), true)?.Any() == true)
                                .Select(p => new { p.Name });
                    if (properties.Count() > 0)
                    {
                        var docParas = swaggerDoc.Paths.Where(p => p.Key.Equals($"/{api.RelativePath}", StringComparison.OrdinalIgnoreCase))
                            .Select(p => p.Value).FirstOrDefault();
                        if (docParas == null)
                        {
                            continue;
                        }
                        switch (api.HttpMethod.ToLower())
                        {
                            case "get":
                                RemovePara(docParas.Get, properties);
                                break;
                            case "post":
                                RemovePara(docParas.Post, properties);
                                break;
                            case "put":
                                RemovePara(docParas.Put, properties);
                                break;
                            case "delete":
                                RemovePara(docParas.Delete, properties);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void RemovePara(Operation operation, IEnumerable<dynamic> properties)
        {
            if (operation == null || operation.Parameters == null)
            {
                return;
            }
            foreach (var prop in properties)
            {
                var docPara = operation.Parameters.FirstOrDefault(dp => dp.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));
                if (docPara != null)
                {
                    operation.Parameters.Remove(docPara);
                }
            }
        }
    }
}
