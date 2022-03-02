namespace ExpressCargo.Core.Extensions;
public static class QueryableExtensions
{
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName, string direction = "ASC")
    {
        return query.OrderingHelper(propertyName, direction, false);
    }

    public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, string propertyName, string direction = "ASC")
    {
        return query.OrderingHelper(propertyName, direction, true);
    }

    private static IOrderedQueryable<T> OrderingHelper<T>(this IQueryable<T> query, string propertyName, string direction, bool anotherLevel)
    {
        ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);

        Expression body = param;
        foreach (var member in propertyName.Split('.'))
        {
            body = Expression.PropertyOrField(body, member);
        }
        var descending = direction.ToUpper() == "DESC";

        LambdaExpression sort = Expression.Lambda(body, param);
        MethodCallExpression call = Expression.Call(
            typeof(Queryable),
            (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
            new[] { typeof(T), body.Type },
            query.Expression,
            Expression.Quote(sort));

        return (IOrderedQueryable<T>)query.Provider.CreateQuery<T>(call);
    }
}