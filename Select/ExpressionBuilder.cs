using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Select
{
    public class ExpressionBuilder
    {
        public async Task<IEnumerable<TModel>> GetOptionsAsync<TModel>(IEnumerable<TModel> data, string value,
            Expression<Func<TModel, string>> identifierProperty, Expression<Func<TModel, string>> nameProperty)
        {
            var visitor = new ParameterModifier();

            var type = typeof(OptionListItem);
            var parameter = Expression.Parameter(typeof (TModel), "i");
            var a = Expression.Lambda(identifierProperty.Type, Expression.MakeMemberAccess(parameter, ((MemberExpression)identifierProperty.Body).Member), parameter);
            var b = Expression.Lambda(nameProperty.Type, Expression.MakeMemberAccess(parameter, ((MemberExpression)nameProperty.Body).Member), parameter);
            //var parameter = identifierProperty.Parameters.First();
            var identityFieldExpression = Expression.Call(a.Body, "ToString", null);
            var nameFieldExpression = b.Body;
            var newExpression = Expression.New(type);
            var initializer = Expression.MemberInit(newExpression,
                Expression.Bind(type.GetProperty("Id"), identityFieldExpression),
                Expression.Bind(type.GetProperty("Name"), nameFieldExpression));
            var selectDelegate = Expression.Lambda(initializer, parameter).Compile();

            throw new NotImplementedException();
        }
    }
}
