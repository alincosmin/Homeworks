using System.Data.Entity;

namespace P10Proj
{
    public class P10Entities : DbContext
    {
        public P10Entities()
            : base("name=P6CodeFirstContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
            .Map<FullTimeEmployee>(m => m.Requires("EmployeeType").HasValue(1))
            .Map<HourlyEmployee>(m => m.Requires("EmployeeType").HasValue(2));
        }

        public virtual DbSet<Employee> Employees { get; set; } 
    }
}
