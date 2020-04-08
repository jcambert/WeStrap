using System;

namespace WeStrap
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UpperCaseAttribute:Attribute
    {
    }
}
