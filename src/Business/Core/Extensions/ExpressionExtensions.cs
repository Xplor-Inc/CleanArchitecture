namespace ExpressCargo.Core.Extensions;
public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var parameter       = Expression.Parameter(typeof(T));
        var leftVisitor     = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
        var left            = leftVisitor.Visit(expr1.Body);
        var rightVisitor    = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
        var right           = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
    }

    public static Expression<Func<T, bool>> AndAlso<T, TNav>(
        this Expression<Func<T, bool>>  expr1,
        Expression<Func<TNav, bool>>    expr2,
        Expression<Func<T, TNav>>       navigationProperty
    )
    {
        var parameter       = Expression.Parameter(typeof(T));
        var leftVisitor     = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
        var left            = leftVisitor.Visit(expr1.Body);
        var navVisitor      = new ReplaceExpressionVisitor(navigationProperty.Parameters[0], parameter);
        var nav             = navVisitor.Visit(navigationProperty.Body);   
        var right           = Expression.Invoke(expr2, nav);

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var parameter       = Expression.Parameter(typeof(T));
        var leftVisitor     = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
        var left            = leftVisitor.Visit(expr1.Body);
        var rightVisitor    = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
        var right           = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.Or(left, right), parameter);
    }

    public static Expression<Func<T, bool>> Or<T, TNav>(
        this Expression<Func<T, bool>>  expr1,
        Expression<Func<TNav, bool>>    expr2,
        Expression<Func<T, TNav>>       navigationProperty
    )
    {
        var parameter   = Expression.Parameter(typeof(T));
        var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
        var left        = leftVisitor.Visit(expr1.Body);
        var navVisitor  = new ReplaceExpressionVisitor(navigationProperty.Parameters[0], parameter);
        var nav         = navVisitor.Visit(navigationProperty.Body);
        var right       = Expression.Invoke(expr2, nav);

        return Expression.Lambda<Func<T, bool>>(Expression.Or(left, right), parameter);
    }
}

class ReplaceExpressionVisitor : ExpressionVisitor
{
    private readonly Expression _newValue;
    private readonly Expression _oldValue;

    public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
    {
        _oldValue = oldValue;
        _newValue = newValue;
    }

    public override Expression Visit(Expression? node)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        return node == _oldValue ? _newValue : base.Visit(node);
    }
}