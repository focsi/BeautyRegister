using BeautyRegister.Views;
using LocalDAL;
using System.Windows;

namespace BeautyRegister
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App:Application
    {
        public App()
        {
            LocalDC.InitInstance();
            ViewManager.InitInstance();
        }
    }
}
