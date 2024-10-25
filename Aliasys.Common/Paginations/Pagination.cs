﻿namespace Aliasys.Common.Paginations
{
    public static class Pagination
    {
        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> sources, int page, int pageSize, out int rowsCount)
        {
            rowsCount = sources.Count();
            return sources.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
