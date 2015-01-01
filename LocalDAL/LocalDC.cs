using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDAL
{
    public class LocalDC
    {
        public LocalDC()
        {
            var context = new BeautyRegisterDC();
            context.Customers.Add( new Customer() {Name="Kiscica", Phone = "+367777" } );
            context.SaveChanges();
            var res = from customer in context.Customers
                      where customer.ID > 0
                      select customer;

        }
    }
}
