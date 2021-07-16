using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DemoCamara.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

namespace DemoCamara.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoView : ContentPage
    {
        public PhotoView()
        {
            InitializeComponent();
            ListImagesUser();
        }

        public async void ListImagesUser()
        {
            var imagesList = await App.SQLiteDb.GetItemsAsync();

            if (imagesList != null)
            {
                listImages.ItemsSource = imagesList;
            }
        }

        private async void btnSaveImage_Clicked(object sender, EventArgs e)
        {
            if (validateData())
            {
                string namePhoto = txtNameImage.Text.Replace(" ", "");

                var ing =await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions {
                    DefaultCamera = CameraDevice.Front, // Doesn't always work on Android, depends on Device
                    AllowCropping = true, // UWP & iOS only,
                    PhotoSize = PhotoSize.Medium, // if Custom, you can set CustomPhotoSize = percentage_value 
                    CompressionQuality = 90,
                    Directory = "DemoCamara",
                    Name = $"{namePhoto}.jpg",
                    SaveToAlbum = true
                });

                Images ima = new Images
                {
                    NameUser = "DefaultUser",
                    NameImage = $"{txtNameImage.Text}.jpg",
                    UrlImage = ing.Path
                };

                await App.SQLiteDb.SaveImagesAsync(ima);
                

                txtNameImage.Text = "";

                await DisplayAlert("Register", "Successfully saved", "Ok");
            }
            else
            {
                await DisplayAlert("Advertency", "It is necessary to enter the name of the image", "Ok");
            }

            ListImagesUser();
        }

        public bool validateData()
        {
            bool response;
            if (string.IsNullOrEmpty(txtNameImage.Text))

            {
                response = false;
            }
            else
            {
                response = true;
            }

            return response;
        }

        private async void btnSaveGallery_Clicked(object sender, EventArgs e)
        {

            if (validateData())
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    string namePhoto = txtNameImage.Text.Replace(" ", "");
                    var file = await CrossMedia.Current.PickPhotoAsync();

                    Images ima = new Images
                    {
                        NameUser = "DefaultUser",
                        NameImage = $"{namePhoto}.jpg",
                        UrlImage = file.Path
                    };

                    await App.SQLiteDb.SaveImagesAsync(ima);

                    await DisplayAlert("Register", "Successfully saved", "Ok");

                    ListImagesUser();

                }
            }
            else
            {
                await DisplayAlert("Advertency", "It is necessary to enter the name of the image", "Ok");
            }
        }
    }
}