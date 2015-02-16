
using BeautyRegister.DataClasses;
using System.Linq;

namespace LocalDAL
{
    public class LocalDC
    {
        private BeautyRegisterDC DataContext;

        public static LocalDC Instance { get; private set; }

        public static void InitInstance()
        {
            Instance = new LocalDC();
        }

        private LocalDC()
        {
            DataContext = new BeautyRegisterDC();
        }
        
        public IQueryable<Customer> GetCustomers()
        {
            return DataContext.Customers;
        }

        public void AddCustomer()
        {
            DataContext.Customers.Add( new Customer() { Name = "Kiscica", Phone = "+367777" } );
            DataContext.SaveChanges();
        }

        public IQueryable<HairColor> GetHairColors()
        {
            return DataContext.HairColors;
        }

        public IQueryable<HairStyle> GetHairStyles()
        {
            return DataContext.HairStyles;  
        }
    }
}
