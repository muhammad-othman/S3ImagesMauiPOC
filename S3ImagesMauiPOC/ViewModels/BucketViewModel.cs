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

[QueryProperty(nameof(BucketName), nameof(BucketName))]
public partial class BucketViewModel : ObservableObject
{
    public string BucketName { get; set; }

    [ObservableProperty]
    ObservableCollection<string> imageUrls;


    private static readonly List<string> ImageExtensions = new List<string> { "JPG", "JPEG", "JPE", "BMP", "GIF", "PNG" };

    public async Task LoadImages()
    {
        Console.WriteLine(BucketName);


        var accessKeyID = await SecureStorage.Default.GetAsync("AccessKeyID");
        var secretAccessKey = await SecureStorage.Default.GetAsync("SecretAccessKey");

        var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(accessKeyID, secretAccessKey);
        using var client = new AmazonS3Client(awsCredentials);
        try
        {
            var imageNames = new List<string>();

            ListObjectsV2Request request = new ListObjectsV2Request
            {
                BucketName = BucketName,
                MaxKeys = 10
            };
            ListObjectsV2Response response;
            do
            {
                response = await client.ListObjectsV2Async(request);

                imageNames.AddRange(response.S3Objects.Select(e => e.Key).Where(e => ImageExtensions.Contains(e.ToUpper().Split(".").Last())));
                request.ContinuationToken = response.NextContinuationToken;

            } while (response.IsTruncated);

            
            var urlList = new List<string>();

            foreach (var imgName in imageNames)
            {
                GetPreSignedUrlRequest preSignedUrlRequest = new GetPreSignedUrlRequest
                {
                    BucketName = BucketName,
                    Key = imgName,
                    Expires = DateTime.UtcNow.AddMinutes(10)
                };

                urlList.Add(client.GetPreSignedURL(preSignedUrlRequest));
            }

            ImageUrls = new ObservableCollection<string>(urlList);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
