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


        #region SelectedCustomer
        public const string SelectedCustomerPropertyName = "SelectedCustomer";

        private Customer m_SelectedCustomer;

        public Customer SelectedCustomer
        {
            get
            {
                return m_SelectedCustomer;
            }

            set
            {
                if (m_SelectedCustomer == value)
                    return;

                m_SelectedCustomer = value;

                // Binding frissítése
                RaisePropertyChanged( SelectedCustomerPropertyName );
            }
        }

        #endregion
        #region OpenCustomerViewCommand

        public RelayCommand OpenCustomerViewCommand
        {
            get
            {
                if (m_OpenCustomerViewCommand == null)
                    m_OpenCustomerViewCommand = new RelayCommand( OpenCustomerViewCommandCall );
                return m_OpenCustomerViewCommand;
            }
        }
        private RelayCommand m_OpenCustomerViewCommand;


        private void OpenCustomerViewCommandCall()
        {
            if (SelectedCustomer == null)
                return;

            BaseView view = new CustomerView();
            view.DataContext = new CustomerViewModel( SelectedCustomer );
            ViewManager.Instance.Open( view );
        }
        #endregion
        
    }
}
