using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace ExpressionTrees.Sort
{
    public static class Sort
    {
        private static readonly MethodInfo OrderByMethod =
            typeof(Queryable).GetMethods().First(s => s.Name == "OrderBy" && s.GetParameters().Length == 2 && s.IsGenericMethod);
        private static readonly MethodInfo OrderByDescendingMethod =
            typeof(Queryable).GetMethods().First(s => s.Name == "OrderByDescending" && s.GetParameters().Length == 2 && s.IsGenericMethod);
        private static readonly MethodInfo ThenByMethod =
            typeof(Queryable).GetMethods()
                .First(s => s.Name == "ThenBy" && s.GetParameters().Length == 2 && s.IsGenericMethod);
        private static readonly MethodInfo ThenByDescendingMethod =
            typeof(Queryable).GetMethods()
                .First(s => s.Name == "ThenByDescending" && s.GetParameters().Length == 2 && s.IsGenericMethod);

        public async static Task<IEnumerable<TModel>> SortAsync<TModel>(IEnumerable<TModel> data, SortCriteria criteria)
        {
            var parameter = Expression.Parameter(typeof (TModel), "i");
            var queryable = data.AsQueryable().Expression;

            if (!criteria.Fields.Any()) return data;

            Expression body = null;
            var fieldNum = 1;
            foreach (var field in criteria.Fields)
            {
                Expression property;
                if (fieldNum == 1)
                {
                    if (field.Direction == "asc")
                    {
                        property = Expression.Property(parameter, field.FieldName);
                        var lambda = Expression.Lambda<Func<TModel, string>>(property, parameter);
                        var orderbyMethod = OrderByMethod.MakeGenericMethod(typeof (TModel), typeof (string));
                        body = Expression.Call(orderbyMethod, data.AsQueryable().Expression, lambda);
                    }
                    else
                    {
                        property = Expression.Property(parameter, field.FieldName);
                        var lambda = Expression.Lambda<Func<TModel, string>>(property, parameter);
                        var orderbyMethod = OrderByDescendingMethod.MakeGenericMethod(typeof(TModel), typeof(string));
                        body = Expression.Call(orderbyMethod, data.AsQueryable().Expression, lambda);
                    }
                }
                else
                {
                    if (field.Direction == "asc")
                    {
                        property = Expression.Property(parameter, field.FieldName);
                        var lambda = Expression.Lambda<Func<TModel, string>>(property, parameter);
                        var orderbyMethod = ThenByMethod.MakeGenericMethod(typeof(TModel), typeof(string));
                        body = Expression.Call(orderbyMethod, body, lambda);
                    }
                    else
                    {
                        property = Expression.Property(parameter, field.FieldName);
                        var lambda = Expression.Lambda<Func<TModel, string>>(property, parameter);
                        var orderbyMethod = ThenByDescendingMethod.MakeGenericMethod(typeof(TModel), typeof(string));
                        body = Expression.Call(orderbyMethod, body, lambda);
                    }
                }
                fieldNum++;
            }

            return data.AsQueryable().Provider.CreateQuery<TModel>(body).ToList();
        }
    }
}
