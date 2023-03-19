using CommunityToolkit.Maui.Alerts;
using System.Diagnostics;

namespace ColorMaker;

public partial class MainPage : ContentPage
{
    private bool _isRandom;
    private string _hexValue;

    public MainPage()
    {
        InitializeComponent();
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (_isRandom) return;
        var red = sldRed.Value;
        var green = sldGreen.Value;
        var blue = sldBlue.Value;

        var color = Color.FromRgb(red, green, blue);

        SetColor(color);
    }

    private void SetColor(Color color)
    {
        Debug.WriteLine(color.ToString());
        btnRandom.BackgroundColor = color;
        Container.BackgroundColor = color;
        _hexValue = color.ToHex();
        lblHex.Text = _hexValue;
    }

    private void BtnRandom_OnClicked(object sender, EventArgs e)
    {
        _isRandom = true;
        var random = new Random();
        var color = Color.FromRgb(
            random.Next(0, 256),
            random.Next(0, 256),
            random.Next(0, 256));

        SetColor(color);

        sldRed.Value = color.Red;
        sldGreen.Value = color.Green;
        sldBlue.Value = color.Blue;
        _isRandom = false;
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(_hexValue);
        var toast = Toast.Make("Color copied",
            CommunityToolkit.Maui.Core.ToastDuration.Short,
            12);
        await toast.Show();
    }
}

