namespace S3ImagesMauiPOC;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(BucketsPage), typeof(BucketsPage));
		Routing.RegisterRoute(nameof(BucketPage), typeof(BucketPage));
    }
}
