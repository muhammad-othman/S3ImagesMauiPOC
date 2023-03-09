using S3ImagesMauiPOC.ViewModels;

namespace S3ImagesMauiPOC;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

