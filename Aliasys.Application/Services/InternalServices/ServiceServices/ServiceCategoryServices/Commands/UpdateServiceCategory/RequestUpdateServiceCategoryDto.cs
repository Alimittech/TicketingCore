namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.UpdateServiceCategory
{
    public class RequestUpdateServiceCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserGroupId_FK { get; set; }
    }
}
