namespace Aliasys.Common.Dtos
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public ActionType ActionType { get; set; }
        public int? Code { get; set; }
    }

    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public ActionType ActionType { get; set; }
        public int? Code { get; set; }
        public T Data { get; set; }
    }

    public enum ActionType
    {
        Null = 0,
        Created = 1,
        Updated = 2,
        Deleted = 3,
        NotExist = 4,
        IsExist = 5,
        Error = 6,
        Failed = 7,
        Completed = 8,
        NotChange = 9,
        Changed = 10
    }
}
