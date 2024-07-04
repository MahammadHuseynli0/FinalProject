using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Extension
{
    public class PaginatedList<T> : List<T>
    {

        public PaginatedList(List<T> values, int count, int pageSize, int page)
        {
            this.AddRange(values);
            ActivePage = page;
            TotalPageCount = (int)Math.Ceiling(count / (double)pageSize);

        }

        public PaginatedList()
        {

        }

        public int TotalPageCount { get; set; }
        public int ActivePage { get; set; }
        public bool HasNext => ActivePage < TotalPageCount;
        public bool HasPrev => ActivePage > 1;

        public static PaginatedList<T> Create(List<T> values, int pageSize, int page)
        {
            return new PaginatedList<T>(values.Skip((page - 1) * pageSize).Take(pageSize).ToList(), values.Count(), pageSize, page);
        }

    }
}
