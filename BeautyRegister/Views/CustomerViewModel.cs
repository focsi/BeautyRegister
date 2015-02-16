using BeautyRegister.Controls;
using BeautyRegister.DataClasses;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace BeautyRegister.Views
{
    class CustomerViewModel : INotifyPropertyChanged
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

        public CustomerViewModel( Customer customer )
        {
            m_Customer = customer;
            HairColors = new List<OptionListItem>( LocalDAL.LocalDC.Instance.GetHairColors().Select( HairColorConvert() ) );
            HairStyles = new List<OptionListItem>( LocalDAL.LocalDC.Instance.GetHairStyles().Select( HairStyleConvert() ) );
        }

        private Expression<System.Func<HairColor, OptionListItem>> HairColorConvert()
        {
            return hc => new OptionListItem() { DisplayValue = hc.Name, Value = hc.ID };
        }

        private Expression<System.Func<HairStyle, OptionListItem>> HairStyleConvert()
        {
            return hs => new OptionListItem() { DisplayValue = hs.Name, Value = hs.ID };
        }

        #region Customer
        public const string CustomerPropertyName = "Customer";

        private Customer m_Customer;

        public Customer Customer
        {
            get
            {
                return m_Customer;
            }

            set
            {
                if (m_Customer == value)
                    return;

                m_Customer = value;

                // Binding frissítése
                RaisePropertyChanged( CustomerPropertyName );
            }
        }

        #endregion


        #region HairColors
        public const string HairColorsPropertyName = "HairColors";

        public List<OptionListItem> HairColors { get; private set;}

        public List<OptionListItem> HairStyles { get; private set; }

        #endregion
    }
}
