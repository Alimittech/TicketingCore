namespace Aliasys.Application.Services.InternalServices.CheckEntityDependencies
{
    public interface ICheckEntityDependency
    {
        List<ResultCheckEntityDependencyDto> Check(RequestCheckEntityDependencyDto request);
    }

    public class ResultCheckEntityDependencyDto
    {
        public string TableName { get; set; }
        public int RecordId { get; set; }
    }

    public class RequestCheckEntityDependencyDto
    {
        public string TableName { get; set; }
        public int ForeignkeyId { get; set; }
    }

    public class CheckEntityDependency : ICheckEntityDependency
    {
        public List<ResultCheckEntityDependencyDto> Check(RequestCheckEntityDependencyDto request)
        {
            throw new NotImplementedException();
        }
    }
}
