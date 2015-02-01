using System.Windows.Controls;

namespace BeautyRegister.Views
{
    public partial class CustomerListView : BaseView
    {
        public CustomerListView()
        {
            DataContext = new CustomerListViewModel();
            InitializeComponent();
        }
    }
}
