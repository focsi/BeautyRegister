using BeautyRegister.DataClasses;
using System.Data.Entity;

namespace LocalDAL
{

     class BeautyRegisterDC : DbContext
    {
        public BeautyRegisterDC()
            : base("name=BeautyRegisterDC")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable( "Customer" );
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
    }
}
