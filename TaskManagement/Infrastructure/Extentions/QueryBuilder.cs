using System.Linq.Expressions;
using System.Collections.Concurrent;

public static class QueryBuilder
{
    private static readonly ConcurrentDictionary<string, LambdaExpression> SearchCache = new();
    private static readonly ConcurrentDictionary<string, LambdaExpression> SortCache = new();

    public static IQueryable<T> ApplySearch<T>(this IQueryable<T> query, Dictionary<string, string> searchTerms)
    {
        if (searchTerms == null || searchTerms.Count == 0)
            return query;

        string cacheKey = $"{typeof(T).FullName}_search_{string.Join("_", searchTerms.Keys)}";

        var predicate = (Expression<Func<T, bool>>)SearchCache.GetOrAdd(cacheKey, _ =>
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression body = Expression.Constant(true);

            foreach (var kvp in searchTerms)
            {
                var property = Expression.Property(parameter, kvp.Key);
                var constant = Expression.Constant(kvp.Value);

                // Convert both to string for Contains (you can adapt for specific types)
                var toStringMethod = typeof(object).GetMethod("ToString");
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                var propertyStr = Expression.Call(property, toStringMethod);
                var constantStr = Expression.Call(constant, toStringMethod);
                var containsExp = Expression.Call(propertyStr, containsMethod, constantStr);

                body = Expression.AndAlso(body, containsExp);
            }

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        });

        return query.Where(predicate);
    }

    public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, Dictionary<string, bool> sortFields)
    {
        if (sortFields == null || sortFields.Count == 0)
            return query;

        IOrderedQueryable<T> orderedQuery = null;
        bool first = true;

        foreach (var sort in sortFields)
        {
            string cacheKey = $"{typeof(T).FullName}_sort_{sort.Key}_{sort.Value}";
            var lambda = (LambdaExpression)SortCache.GetOrAdd(cacheKey, _ =>
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, sort.Key);
                var propertyType = property.Type;

                var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), propertyType);
                return Expression.Lambda(delegateType, property, parameter);
            });

            if (first)
            {
                orderedQuery = sort.Value
                    ? Queryable.OrderBy(query, (dynamic)lambda)
                    : Queryable.OrderByDescending(query, (dynamic)lambda);
                first = false;
            }
            else
            {
                orderedQuery = sort.Value
                    ? Queryable.ThenBy(orderedQuery, (dynamic)lambda)
                    : Queryable.ThenByDescending(orderedQuery, (dynamic)lambda);
            }
        }

        return orderedQuery ?? query;
    }
}
