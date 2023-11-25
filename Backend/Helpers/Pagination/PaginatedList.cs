using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Webcomic.Models.DTOs.ComicDtos;

namespace Webcomic.Helpers.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling((double)count / pageSize);

            AddRange(items);
        }

        public static async Task<PaginatedList<TDestination>> Create<TSource, TDestination>(IQueryable<TSource> source, int pageNumber, int pageSize, IMapper mapper)
        {
            var count = await source.CountAsync();
            var totalPages = (int)Math.Ceiling((double)count / pageSize);

            if (pageNumber < 1 || pageNumber > totalPages)
            {
                pageNumber = 1; // Trở về trang đầu nếu trang yêu cầu không hợp lệ
            }

            var items = await source.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            var mappedItems = mapper.Map<List<TDestination>>(items);

            return new PaginatedList<TDestination>(mappedItems, count, pageNumber, pageSize);
        }
    }
}
