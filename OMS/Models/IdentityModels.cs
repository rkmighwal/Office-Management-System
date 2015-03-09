using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tasks>().Property(t => t.PercentComplete).HasPrecision(3, 2);

            modelBuilder.Entity<TaskDependency>()
                    .HasRequired(td => td.Successor)
                    .WithMany(t => t.SuccessorTasks)
                    .HasForeignKey(td => td.SuccessorID)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                    .HasOptional(t => t.ParentTask)
                    .WithMany()
                    .HasForeignKey(td => td.ParentID);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Leaves> Leaves { get; set; }

        public DbSet<LeaveApplications> LeaveApplications { get; set; }

        public DbSet<LeaveApprovals> LeaveApprovals { get; set; }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<Resource> Resource { get; set; }

        public DbSet<ResourceAssignment> ResourceAssignment { get; set; }

        public DbSet<TaskDependency> TaskDependency { get; set; }

    }
}
