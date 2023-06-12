using CommunityToolkit.Mvvm.Input;
using MeldPuntIncidentApp.Services;
using System.Diagnostics;
using Shared.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Controls;
using MeldPuntIncidentApp.View;
using System.Xml.Linq;
using SkiaSharp;

namespace MeldPuntIncidentApp.ViewModel;

public partial class IncidentViewModel : BaseViewModel
{

    public MainViewModel mainViewModel;
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    private readonly IIncidentService _incidentService;

    public bool IsChecked { get; set; }

    [ObservableProperty]
    public string description;

    [ObservableProperty]
    public string imageUrl;

    [ObservableProperty]
    public string category;

    [ObservableProperty]
    public DateTime date = DateTime.Now;

    public IncidentViewModel(IIncidentService incidentService)
    {
        Title = "New Incident";
        _incidentService = incidentService;
    }

    [RelayCommand]
    public async Task Submit()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;

            Location location = null;

            if (IsChecked)
            {
                location = await GetCurrentLocation();
            }

            var incident = new IncidentItemDto
            {
                Description = Description,
                Category = Category,
                Date = Date,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                ImageUrl = ImageUrl
            };

            Incidents.Add(incident);

            var response = await _incidentService.CreateIncident(incident);

            if (response != null)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Incident added", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"{e}");
            await App.Current.MainPage.DisplayAlert("error", e.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    async Task<Location> GetCurrentLocation()
    {
        Location location = null;
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            _isCheckingLocation = false;
        }

        return location;
    }

    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }

    [RelayCommand]
    public async void TakePhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // Read the image data from the captured photo
                using Stream sourceStream = await photo.OpenReadAsync();
                byte[] imageData = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(imageData, 0, imageData.Length);

                // Decode the image data into a bitmap
                SKBitmap bitmap = SKBitmap.Decode(imageData);

                // Resize the bitmap
                int maxWidth = 800;
                int maxHeight = 600;
                double scale = Math.Min((double)maxWidth / bitmap.Width, (double)maxHeight / bitmap.Height);
                int newWidth = (int)(bitmap.Width * scale);
                int newHeight = (int)(bitmap.Height * scale);
                SKBitmap resizedBitmap = bitmap.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.High);

                // Encode the bitmap in a compressed format
                using SKData encodedData = resizedBitmap.Encode(SKEncodedImageFormat.Jpeg, 75);
                byte[] compressedImageData = encodedData.ToArray();

                // Convert the compressed image data to a base64 string
                string base64Image = Convert.ToBase64String(compressedImageData);
                ImageUrl = base64Image;
            }
        }
    }

}



