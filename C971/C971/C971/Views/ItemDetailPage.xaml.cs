using System.ComponentModel;
using Xamarin.Forms;
using C971.ViewModels;

namespace C971.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}