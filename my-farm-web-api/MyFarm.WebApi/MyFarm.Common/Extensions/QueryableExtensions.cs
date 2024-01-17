namespace MyFarm.Common.Extensions
{
    /// <summary>
    /// Contains extension methods to <see cref="IQueryable{T}"/> for configuring services.
    /// </summary>
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageSize, int pageIndex) =>
           query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }
}
