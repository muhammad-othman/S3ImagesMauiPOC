using S3ImagesMauiPOC.ViewModels;

namespace S3ImagesMauiPOC;

public partial class BucketsPage : ContentPage
{
    BucketsViewModel _vm;
    public BucketsPage(BucketsViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _vm.LoadBuckets();
    }
}