using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Collection
{
    public interface IPagedList
    {
        int TotalCount { get; }
        bool HasMorePages { get; }
        bool HasPrevPages { get; }
        int TotalPages { get; }
        int Page { get; }
        int ItemPerPage { get; }
        int NextPage { get; }
        int PrevPage { get; }
        string Sort { get; }

    }
}
