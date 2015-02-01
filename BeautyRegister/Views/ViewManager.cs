using BeautyRegister.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BeautyRegister.Views
{
    class ViewManager
    {
        #region Singleton pattern

        private static ViewManager m_Instance = null;

        private ViewManager()
        {
        }

        internal static void InitInstance()
        {
            m_Instance = new ViewManager();
        }

        internal static ViewManager Instance
        {
            get
            {
                return m_Instance;
            }
        }
        #endregion

        #region Properties, members

        internal MainWindowViewModel Parent { get; set; }

        private List<BaseView> m_ViewsLIFO = new List<BaseView>();
        #endregion

        #region Methodes

        internal void Open( BaseView view )
        {
            // hozzáadjuk a lista végéhez
            m_ViewsLIFO.Add( view );

            // a szülőnek beállítjuk, hogy ki látszódjon
            Parent.MainView = view;

            // van neki szubmenuje?
            ISubMenuOwner subMenuOwner = view as ISubMenuOwner;
            if (subMenuOwner != null)
                Parent.SubMenu = subMenuOwner.SubMenu;
            else
                Parent.SubMenu = null;

            // tudatjuk a világgal, hogy megnyitottunk
      //      Messenger.Default.Send<Messages.ViewOpenClose>( new Messages.ViewOpenClose( true ) );

        }

        internal void Back()
        {
            // nem sikerült bezárni?
            if (false == Parent.MainView.Close())
                return;

            // az aktuális ablakot kiszedjuk a listából ( fura lenne, ha nem az utolsó lenne )
            m_ViewsLIFO.Remove( Parent.MainView );

            // van korábbi ablak?
            if (m_ViewsLIFO.Count > 0)
            {
                // az előző ablak visszaállítása
                Parent.MainView = m_ViewsLIFO.Last();

                // van neki szubmenuje?
                ISubMenuOwner subMenuOwner = Parent.MainView as ISubMenuOwner;
                if (subMenuOwner != null)
                    Parent.SubMenu = subMenuOwner.SubMenu;
                else
                    Parent.SubMenu = null;
            }
            else
            {
                // nincs korábbi ablak
                Parent.MainView = null;
                Parent.SubMenu = null;
            }

            // tudatjuk a világgal, hogy bezártunk egy ablakot
            //Messenger.Default.Send<Messages.ViewOpenClose>( new Messages.ViewOpenClose( false ) );
        }
        #endregion
    }
}
