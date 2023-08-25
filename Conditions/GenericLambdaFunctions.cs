using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenerateLambda.Conditions
{
    public static class GenericLambdaFunctions
    {
        public static Func<T, string> CreateGroupByKeySelector<T>(string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            MemberExpression property = Expression.Property(parameter, propertyName);
            Expression<Func<T, string>> lambda = Expression.Lambda<Func<T, string>>(property, parameter);

            return lambda.Compile();
        }

        public static Func<T, bool> CreateWhereCondition<T>(string propertyName, string filterOperator, string filterValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            MemberExpression property = Expression.Property(parameter, propertyName);
            ConstantExpression value = Expression.Constant(filterValue);

            ExpressionType expressionType = ExpressionType.Equal;
            if (filterOperator == "!=")
                expressionType = ExpressionType.NotEqual;
            // Diğer operatörleri burada kontrol edebilirsiniz...

            BinaryExpression conditionExpression = Expression.MakeBinary(expressionType, property, value);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(conditionExpression, parameter);

            return lambda.Compile();
        }
    }
}
