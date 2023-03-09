using S3ImagesMauiPOC.ViewModels;

namespace S3ImagesMauiPOC;

public partial class BucketPage : ContentPage
{
    BucketViewModel _vm;
    public BucketPage(BucketViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _vm.LoadImages();
    }
}