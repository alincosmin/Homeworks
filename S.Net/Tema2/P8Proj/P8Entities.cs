using System.Data.Entity;

namespace P8Proj
{
    public class P8Entities : DbContext
    {
        public P8Entities() 
            : base("name=P6CodeFirstContext")
        {
        }

        public virtual DbSet<Business> Businesses { get; set; } 
    }
}