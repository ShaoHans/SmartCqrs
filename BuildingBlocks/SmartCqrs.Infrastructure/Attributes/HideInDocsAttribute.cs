using System;

namespace SmartCqrs.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property)]
    public class HideInDocsAttribute : Attribute
    {
    }
}
