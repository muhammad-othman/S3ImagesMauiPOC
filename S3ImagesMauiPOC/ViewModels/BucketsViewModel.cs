using Amazon.S3;
using Amazon.S3.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3ImagesMauiPOC.ViewModels;

public partial class BucketsViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<string> buckets;

    public async Task LoadBuckets()
    {

        var accessKeyID = await SecureStorage.Default.GetAsync("AccessKeyID");
        var secretAccessKey = await SecureStorage.Default.GetAsync("SecretAccessKey");

        var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(accessKeyID, secretAccessKey);
        AmazonS3Client client = new AmazonS3Client(awsCredentials);
        try
        {

            ListBucketsResponse response = await client.ListBucketsAsync();

            Buckets = new ObservableCollection<string>(response.Buckets.Select(e => e.BucketName));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    [RelayCommand]
    async Task GoToBucket(string BucketName)
    {
        try
        {
            await Shell.Current.GoToAsync($"{nameof(BucketPage)}?{nameof(BucketName)}={BucketName}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
