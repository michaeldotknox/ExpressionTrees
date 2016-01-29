using System.Linq.Expressions;

namespace Select
{
    public class ParameterModifier : ExpressionVisitor
    {
        public Expression Modify(Expression expression, ParameterExpression parameter)
        {
            return Visit(expression, parameter);
        }

        public Expression Visit(Expression expression, ParameterExpression parameter)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return VisitMember((MemberExpression)expression, parameter);
            }
            return base.Visit(expression);
        }

        private static Expression VisitMember(MemberExpression expression, ParameterExpression parameter)
        {
            return Expression.MakeMemberAccess(parameter, expression.Member);
        }
    }
}
