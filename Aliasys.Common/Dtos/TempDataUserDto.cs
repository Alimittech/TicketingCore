namespace Aliasys.Common.Dtos
{
    public class TempDataUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ExtentionNumber { get; set; }
        public int? PersonCode { get; set; }
        public bool IsActive { get; set; }
        public string Organiztion { get; set; }
        public string OperationUnit { get; set; }
        public string Position { get; set; }
        public string Manager { get; set; }
        public string? Roll { get; set; }
        public List<int>? UserGroups { get; set; }
        public string? ImageName { get; set; }
    }
}
