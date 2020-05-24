using System;
using System.Linq.Expressions;

namespace WeExpression
{
    public static class Extension
    {
        public static Expression<Func<T,bool>> Parse<T>(this string value) =>null;
    }
}
