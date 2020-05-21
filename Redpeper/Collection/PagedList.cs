using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Collection
{
    public class PagedList<T> : List<T>, IPagedList
    {
        public int TotalPages { get; protected set; }

        public PagedList(List<T> items, int count, int pageIndex, int pageSize, string sort)
        {
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Page = pageIndex;
            ItemPerPage = pageSize;
            PrevPage = (Page <= 1) ? 1 : Page - 1;
            NextPage = (Page < TotalPages) ? Page + 1 : TotalPages;
            Sort = sort;
            TotalCount = count;
            AddRange(items);
        }


        public int TotalCount { get; protected set; }

        public bool HasMorePages => (Page < TotalPages);

        public bool HasPrevPages => (Page > 1);

        public int Page { get; protected set; }

        public int ItemPerPage { get; protected set; }

        public int NextPage { get; protected set; }

        public int PrevPage { get; protected set; }

        public string Sort { get; set; }

    }
}
