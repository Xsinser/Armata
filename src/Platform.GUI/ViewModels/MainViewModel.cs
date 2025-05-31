using Avalonia.Media.Imaging;
using OpenCvSharp;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Platform.GUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";


    private Bitmap _bitmapCap;
    public Bitmap BitmapCap
    {
        get => _bitmapCap;
        set
        {
            _bitmapCap = value;
            OnPropertyChanged();
        }
    }

    VideoCapture _capture;

    public MainViewModel()
    {
        _capture = new VideoCapture(0);
        if (_capture.IsOpened())
        {
            Task.Run(() =>
            {
                while (_capture.IsOpened())
                {
                    // возможно нужно оптимизировать для уменьшения нагрузки цпу
                    Mat mat = new Mat();
                    _capture.Read(mat);
                    BitmapCap = new Bitmap(mat.ToMemoryStream());
                }
            });
        }
    }
}
