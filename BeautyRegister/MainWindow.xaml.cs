using LocalDAL;
using System.Windows;
using System.Linq;

namespace BeautyRegister
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow:Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LocalDC.InitInstance();

            var a = from customer in LocalDC.Instance.GetCustomers()
                    select customer.Name;
            
            list.ItemsSource = a.ToList() ;
        }

        private void list_SelectionChanged( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
        {

        }
    }
}
