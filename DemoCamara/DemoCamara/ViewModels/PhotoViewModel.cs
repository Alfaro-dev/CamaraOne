using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

using DemoCamara.Services;

namespace DemoCamara.ViewModels
{
    public class PhotoViewModel : BaseViewModel
    {
        CameraService cameraService;

        private ImageSource _photo;

        public ImageSource Photo
        {
            get { return _photo; }
            set { _photo = value; OnPropertyChanged(); }
        }

        public ICommand TakePhotoCommand { get; private set; }
        public ICommand ChoosePhotoCommand { get; private set; }

        public PhotoViewModel()
        {
            cameraService = new CameraService();
            Task.Run(async () => await cameraService.Init());

            /// TakePhotoCommand = new Command(async () => await TakePhoto(string namePhoto));
            ChoosePhotoCommand = new Command(async () => await ChoosePhoto());
        }

        public async Task TakePhoto(string photo)
        {
            var file = await cameraService.TakePhoto(photo);

            if (file != null)
                Photo = ImageSource.FromStream(() => file.GetStream());
        }

        private async Task ChoosePhoto()
        {
            var file = await cameraService.ChoosePhoto();

            if (file != null)
                Photo = ImageSource.FromStream(() => file.GetStream());
        }
    }
}
