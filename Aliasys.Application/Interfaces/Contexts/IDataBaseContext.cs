using Aliasys.Domain.Entities.OperationUnitEntities;
using Aliasys.Domain.Entities.OrganizationEntities;
using Aliasys.Domain.Entities.PositionEntitis;
using Aliasys.Domain.Entities.RegionEntities;
using Aliasys.Domain.Entities.ServiceEntities;
using Aliasys.Domain.Entities.SystemComponentEntities;
using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace Aliasys.Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {
        DbSet<Region> Regions { get; set; }
        DbSet<Organization> Organizations { get; set; }
        DbSet<OperationUnit> OperationUnits { get; set; }
        DbSet<OperationUnitDependency> OperationUnitDependencies { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<SystemComponent> SystemComponents { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserInOrgOpunitPos> UserInOrgOpunitPoses { get; set; }
        DbSet<UserGroup> UserGroups { get; set; }
        DbSet<UserInGroup> UserInGroups { get; set; }
        DbSet<UserRoll> UserRolls { get; set; }
        DbSet<UserInRoll> UserInRolls { get; set; }
        DbSet<ServiceCategory> ServiceCategories { get; set; }
        DbSet<ServiceRequestType> ServiceRequestTypes { get; set; }
        DbSet<ServiceState> ServiceStates { get; set; }
        DbSet<ServicePhase> ServicePhases { get; set; }
        DbSet<ServiceRequest> ServiceRequests { get; set; }
        DbSet<ServiceRequestChild> ServiceRequestChildren { get; set; }
        DbSet<ServiceRequestLifeCycle> ServiceRequestLifeCycles { get; set; }
        DbSet<ServiceRootCause> ServiceRootCauses { get; set; }
        DbSet<ServiceSubCause> ServiceSubCauses { get; set; }
        DbSet<ServiceCauseCategory> ServiceCauseCategories { get; set; }


        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
