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
            InitializeComponent();

            LocalDC.InitInstance();

            var a = from customer in LocalDC.Instance.GetCustomers()
                    select customer.Name;
            
            list.ItemsSource = a.ToList() ;
        }

        private void list_SelectionChanged( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
        {
            if ( e.AddedItems.Count > 0 )
            {
                var name = e.AddedItems[0] as string;
                var cust = (from customer in LocalDC.Instance.GetCustomers()
                           where customer.Name == name
                           select customer).FirstOrDefault();

    
                if (cust != null)
                    nameTB.Text = cust.Phone;
            }
            
        }
    }
}
