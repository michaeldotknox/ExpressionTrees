using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Search
{
    public static class Search
    {
        public async static Task<IEnumerable<TModel>> SearchAsync<TModel>(IEnumerable<TModel> data, SearchCriteria criteria)
        {
            var clauses = new List<Expression>();

            var parameter = Expression.Parameter(typeof(TModel), "i");

            foreach (var field in criteria.Fields)
            {
                if (field.Operator == "equal")
                {
                    Expression clause = Expression.Equal(Expression.Property(parameter, field.FieldName), Expression.Constant(field.Value));
                    clauses.Add(clause);
                }
            }

            if (clauses.Count == 0) return data;

            var body = clauses[0];

            if (clauses.Count > 1)
            {
                body = Expression.And(clauses[0], clauses[1]);
                if (clauses.Count > 2)
                {
                    for (var clauseNum = 2; clauseNum < clauses.Count; clauseNum++)
                    {
                        body = Expression.AndAlso(body, clauses[clauseNum]);
                    }
                }
            }

            var lambda = Expression.Lambda<Func<TModel, bool>>(body, parameter);

            return data.AsQueryable().Where(lambda);
        }
    }
}
