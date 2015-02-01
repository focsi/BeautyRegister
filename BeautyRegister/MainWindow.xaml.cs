using LocalDAL;
using System.Windows;
using System.Linq;
using BeautyRegister.DataClasses;

namespace BeautyRegister
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow:Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            
            InitializeComponent();
        }
    }
}
