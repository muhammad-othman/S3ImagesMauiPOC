using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3ImagesMauiPOC.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IConfiguration configuration;
    [ObservableProperty]
    string accessKeyID;

    [ObservableProperty]
    string secretAccessKey;

    private Task _initializationTask;
    public MainViewModel(IConfiguration _configuration)
    {
        _initializationTask = Init();
        configuration = _configuration;
    }

    private async Task Init()
    {
        AccessKeyID = await SecureStorage.Default.GetAsync(nameof(AccessKeyID));
        SecretAccessKey = await SecureStorage.Default.GetAsync(nameof(SecretAccessKey));

        await Login();
    }

    [RelayCommand]
    async Task Login()
    {
        if (string.IsNullOrWhiteSpace(AccessKeyID) || string.IsNullOrWhiteSpace(SecretAccessKey))
        {
            string accessKeyID = configuration["AccessKeyID"];
            string secretAccessKey = configuration["SecretAccessKey"];

            if (string.IsNullOrWhiteSpace(accessKeyID) || string.IsNullOrWhiteSpace(secretAccessKey))
                return;

            AccessKeyID = accessKeyID;
            SecretAccessKey = secretAccessKey;
        }

        await SecureStorage.Default.SetAsync(nameof(AccessKeyID), AccessKeyID);
        await SecureStorage.Default.SetAsync(nameof(SecretAccessKey), SecretAccessKey);

        await Shell.Current.GoToAsync($"{nameof(BucketsPage)}");

        AccessKeyID = "";
        SecretAccessKey = "";
    }
}
