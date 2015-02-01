using BeautyRegister.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BeautyRegister.Views
{
    public class BaseView : UserControl
    {
        public BaseView()
        {
            // nyelv beállítása magyarra
            this.Language = System.Windows.Markup.XmlLanguage.GetLanguage( "hu-HU" );
        }
        /// <summary>
        /// A view becsukását végző metódus
        /// </summary>
        /// <returns>true - ha sikerült bezárni</returns>
        internal virtual bool Close()
        {
            IClosable closabe = DataContext as IClosable;
            // a datacontext ICloseable?
            if (closabe != null)
                return closabe.Close();
            else
                return false; // default, hogy vegyük észre mikor nem írtuk meg
        }

        /// <summary>
        /// Be lehet-e zárni az ablakot?
        /// </summary>
        /// <returns></returns>
        internal virtual bool IsCloseEnabled()
        {
            IClosable closabe = DataContext as IClosable;
            // a datacontext ICloseable?
            if (closabe != null)
                return closabe.IsCloseEnabled();
            else
                return false; // default, hogy vegyük észre mikor nem írtuk meg
        }
    }
}
