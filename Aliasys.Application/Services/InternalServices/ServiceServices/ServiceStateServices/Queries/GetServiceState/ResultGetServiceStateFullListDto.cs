﻿namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.GetServiceState
{
    public class ResultGetServiceStateFullListDto
    {
        public List<RequestGetServiceStateFullListDto> srvStateList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
