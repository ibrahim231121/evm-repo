using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Microsoft.EntityFrameworkCore;

namespace Crossbones.ALPR.Common
{
    public static class QueryExtension
    {
        public static async Task<PagedResponse<T>> ToPagedListAsync<T>(this IQueryable<T> source, Pager pagination, GridSort sort, CancellationToken token)
        {
            if (sort != null)
                source = source.OrderBy(sort.Field + " " + sort.Dir);

            int page = 1, size = 25, skip = 0, count = 1;
            if (pagination != null)
            {
                page = pagination.Page <= 0 ? 1 : pagination.Page;
                size = pagination.Size <= 0 ? 25 : pagination.Size;
                skip = (pagination.Page - 1) * pagination.Size;
                count = await source.CountAsync(token);
            }
            var items = await source.Skip(skip).Take(size).ToListAsync(token);
            return PaginationHelper.GetPagedResponse(items, count);
        }
        public static async Task<PageResponse<T>> ToFilteredPagedSortedListAsync<T>(this IQueryable<T> source, GridFilter filter, Pager pagination, GridSort sort, CancellationToken token)
        {
            if (filter != null && (filter.Filters != null && filter.Filters.Count > 0))
                ProcessDataUtil.ProcessFilters(filter, ref source);

            if (sort != null && sort.Field != null && sort.Dir != null)
                source = source.OrderBy(sort.Field + " " + sort.Dir);

            int page = 1, size = 25, skip = 0, count = 1;
            if (pagination != null)
            {
                page = pagination.Page <= 0 ? 1 : pagination.Page;
                size = pagination.Size <= 0 ? 25 : pagination.Size;
                skip = (pagination.Page - 1) * pagination.Size;
                count = await source.CountAsync(token);
            }
            var items = await source.Skip(skip).Take(size).ToListAsync(token);
            return new PageResponse<T>(items, count);
        }
        public static PageResponse<T> ToFilteredPagedList<T>(this IQueryable<T> source, GridFilter filter, Pager pagination, GridSort sort)
        {
            if (filter != null && (filter.Filters != null && filter.Filters.Count > 0))
                ProcessDataUtil.ProcessFilters(filter, ref source);

            if (sort != null)
                source = source.OrderBy(sort.Field + " " + sort.Dir);

            int page = 1, size = 25, skip = 0, count = 1;
            if (pagination != null)
            {
                page = pagination.Page <= 0 ? 1 : pagination.Page;
                size = pagination.Size <= 0 ? 25 : pagination.Size;
                skip = (pagination.Page - 1) * pagination.Size;
                count = source.Count();
            }
            var items = source.Skip(skip).Take(size).ToList();
            return new PageResponse<T>(items, count);
        }

        public static async Task<List<T>> ToFilteredSortedListAsync<T>(this IQueryable<T> source, GridFilter filter, GridSort sort, CancellationToken token)
        {
            if (filter != null && (filter.Filters != null && filter.Filters.Count > 0))
                ProcessDataUtil.ProcessFilters(filter, ref source);

            if (sort != null && sort.Field != null && sort.Dir != null)
                source = source.OrderBy(sort.Field + " " + sort.Dir);


            return await source.ToListAsync();
        }
    }
}
