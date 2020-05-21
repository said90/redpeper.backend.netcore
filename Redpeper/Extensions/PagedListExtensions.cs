using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Collection;

namespace Redpeper.Extensions
{
    public static class PagedListExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int page = 1, int itemPerPage = 10, string sort = "")
        {
            var totalItems = source.Count();
            var items = source.Skip((page - 1) * itemPerPage).Take(itemPerPage).ToList();
            return new PagedList<T>(items, totalItems, page, itemPerPage, sort);
        }

        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int page = 1, int itemPerPage = 10, string sort = "")
        {
            if (!string.IsNullOrEmpty(sort)) source = source.OrderBy(sort);
            var totalItems = await source.CountAsync();
            var items = await source
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToListAsync();
            return new PagedList<T>(items, totalItems, page, itemPerPage, sort);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int page = 1, int itemPerPage = 10, string sort = "")
        {
            if (!string.IsNullOrEmpty(sort)) source = source.OrderBy(sort);
            var totalItems = source.Count();
            var items = source.Skip((page - 1) * itemPerPage).Take(itemPerPage).ToList();
            return new PagedList<T>(items, totalItems, page, itemPerPage, sort);
        }
    }

}
