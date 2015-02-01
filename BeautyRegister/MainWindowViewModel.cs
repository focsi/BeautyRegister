using BeautyRegister.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BeautyRegister
{
    class MainWindowViewModel : INotifyPropertyChanged
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

        #region MainView

        /// <summary>
        /// MainView nevének beállítása, hogy ne legyen elírás a RaisePropery-ben
        /// </summary>
        public const string MainViewPropertyName = "MainView";

        /// <summary>
        /// MainView member változója
        /// </summary>
        private BaseView m_MainView;

        /// <summary>
        /// A fő abalak azon része, ahova az oldalak kerülnek
        /// </summary>
        public BaseView MainView
        {
            get
            {
                return m_MainView;
            }

            set
            {
                if (m_MainView == value)
                    return;

                m_MainView = value;

                // Binding frissítése
                RaisePropertyChanged( MainViewPropertyName );
            }
        }
        #endregion

        #region SubMenu

        /// <summary>
        /// SubMenu nevének beállítása, hogy ne legyen elírás a RaisePropery-ben
        /// </summary>
        public const string SubMenuPropertyName = "SubMenu";

        /// <summary>
        /// SubMenu member változója
        /// </summary>
        private UserControl m_SubMenu;

        /// <summary>
        /// A view-khoz tartozó almenü
        /// </summary>
        public UserControl SubMenu
        {
            get
            {
                return m_SubMenu;
            }

            set
            {
                if (m_SubMenu == value)
                    return;

                m_SubMenu = value;

                // Binding frissítése
                RaisePropertyChanged( SubMenuPropertyName );
            }
        }

        #endregion

        public MainWindowViewModel()
        {
            ViewManager.Instance.Parent = this;
            ViewManager.Instance.Open( new CustomerView() );
        }
    }
}
