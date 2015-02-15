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
            modelBuilder.Entity<HairStyle>().ToTable( "HairStyle" );
            modelBuilder.Entity<HairColor>().ToTable( "hairColor" );
        }
    
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<HairStyle> HairStyles { get; set; }

         public virtual DbSet<HairColor> HairColors { get; set; }
    }
}
