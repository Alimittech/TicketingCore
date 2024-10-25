namespace Aliasys.Common.Dtos
{
    public class PaginationDto
    {
        public string SearchKey { get; set; }
        public int PageSize { get; set; } = 5;
        public int Page { get; set; } = 1;

        //public bool showSearckey;
    }
}
