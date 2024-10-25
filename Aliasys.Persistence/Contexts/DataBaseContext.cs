using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Domain.Entities.OperationUnitEntities;
using Aliasys.Domain.Entities.OrganizationEntities;
using Aliasys.Domain.Entities.PositionEntitis;
using Aliasys.Domain.Entities.RegionEntities;
using Aliasys.Domain.Entities.ServiceEntities;
using Aliasys.Domain.Entities.SystemComponentEntities;
using Aliasys.Domain.Entities.UserEntities;
using Aliasys.Persistence.Configurations.OperationUnitConfigs;
using Aliasys.Persistence.Configurations.OrganizationConfigs;
using Aliasys.Persistence.Configurations.PositionConfigs;
using Aliasys.Persistence.Configurations.RegionConfigs;
using Aliasys.Persistence.Configurations.ServiceConfigs;
using Aliasys.Persistence.Configurations.SystemComponentConfigs;
using Aliasys.Persistence.Configurations.UserConfigs;
using Microsoft.EntityFrameworkCore;

namespace Aliasys.Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OperationUnit> OperationUnits { get; set; }
        public DbSet<OperationUnitDependency> OperationUnitDependencies { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<SystemComponent> SystemComponents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInOrgOpunitPos> UserInOrgOpunitPoses { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserInGroup> UserInGroups { get; set; }
        public DbSet<UserRoll> UserRolls { get; set; }
        public DbSet<UserInRoll> UserInRolls { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ServiceRequestType> ServiceRequestTypes { get; set; }
        public DbSet<ServiceState> ServiceStates { get; set; }
        public DbSet<ServicePhase> ServicePhases { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<ServiceRequestChild> ServiceRequestChildren { get; set; }
        public DbSet<ServiceRequestLifeCycle> ServiceRequestLifeCycles { get; set; }
        public DbSet<ServiceRootCause> ServiceRootCauses { get; set; }
        public DbSet<ServiceSubCause> ServiceSubCauses { get; set; }
        public DbSet<ServiceCauseCategory> ServiceCauseCategories { get; set; }

        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Config(modelBuilder);
        }

        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegionConfig());
            modelBuilder.ApplyConfiguration(new OrganizationConfig());
            modelBuilder.ApplyConfiguration(new OperationUnitConfig());
            modelBuilder.ApplyConfiguration(new OperationUnitDependencyConfig());
            modelBuilder.ApplyConfiguration(new PositionConfig());
            modelBuilder.ApplyConfiguration(new SystemComponentConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserInOrgOpunitPosConfig());
            modelBuilder.ApplyConfiguration(new UserRollConfig());
            modelBuilder.ApplyConfiguration(new UserInRollConfig());
            modelBuilder.ApplyConfiguration(new UserGroupConfig());
            modelBuilder.ApplyConfiguration(new UserInGroupConfig());
            modelBuilder.ApplyConfiguration(new ServiceCategoryConfig());
            modelBuilder.ApplyConfiguration(new ServiceRequestTypeConfig());
            modelBuilder.ApplyConfiguration(new ServiceStateConfig());
            modelBuilder.ApplyConfiguration(new ServicePhaseConfig());
            modelBuilder.ApplyConfiguration(new ServiceRequestConfig());
            modelBuilder.ApplyConfiguration(new ServiceRequestChildConfig());
            modelBuilder.ApplyConfiguration(new ServiceRequestLifeCycleConfig());
            modelBuilder.ApplyConfiguration(new ServiceRootCauseConfig());
            modelBuilder.ApplyConfiguration(new ServiceSubCauseConfig());
            modelBuilder.ApplyConfiguration(new ServiceCauseCategoryConfig());
        }
    }
}
