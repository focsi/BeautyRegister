using BeautyRegister.DataClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyRegister.Views
{
    class CustomerListViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged interfész
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged( string propertyName )
        {
            if (PropertyChanged != null)
            {
                PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }
        #endregion

        public CustomerListViewModel()
        {
            Customers = new ObservableCollection<Customer>( LocalDAL.LocalDC.Instance.GetCustomers() );
        }
        #region Customers
        public const string CustomersPropertyName = "Customers";

        private ObservableCollection<Customer> m_Customers;

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return m_Customers;
            }

            set
            {
                if (m_Customers == value)
                    return;

                m_Customers = value;

                // Binding frissítése
                RaisePropertyChanged( CustomersPropertyName );
            }
        }

        #endregion
    }
}
