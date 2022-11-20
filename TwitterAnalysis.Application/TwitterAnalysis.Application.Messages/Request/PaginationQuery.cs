using System;

namespace TwitterAnalysis.Application.Messages.Request
{
    public class PaginationQuery
    {
        public PaginationQuery()
        {
            PageSize = 10;
        }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
