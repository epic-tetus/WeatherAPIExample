using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x412에 나와 있습니다.

namespace WeatherAPIExample
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadingRing.Visibility = Visibility.Visible;
            GetPosition();
        }

        public async void GetPosition()
        {
            Geoposition position = null;
            for(int i = 0; i < 4; i++)
            {
                try
                {
                    position = await LocationManager.GetPosition();
                    Debug.WriteLine("Clear");
                }
                catch (Exception e)
                {
                    //MessageDialog ErrorMessage = new MessageDialog("Please Allow GPS");
                    Debug.WriteLine(e.StackTrace);
                }
            }

            //NowLocation.Text = position.Coordinate.Latitude + ", " + position.Coordinate.Longitude;


            //NowLocation.Text = 
            GetAddress(position.Coordinate.Latitude, position.Coordinate.Longitude);

            GetWeather(position.Coordinate.Latitude, position.Coordinate.Longitude);

            LoadingRing.Visibility = Visibility.Collapsed;
            MainPanel.Visibility = Visibility.Visible;
        }

        public async void GetWeather(double Latitude, double Longitude)
        {
            RootObject NowWeather = await WeatherElement.GetWeather(Latitude,Longitude);

            WeatherResult.Text = NowWeather.name + " - " + (NowWeather.main.temp - 273.15) + " - " + NowWeather.weather[0].description;
        }

        public async void GetAddress(double Latitude, double Longitude)
        {
            AddressObject addressObject = await AddressElement.GetAddress(Latitude, Longitude);

            NowLocation.Text = addressObject.fullName + "의 현재 날씨";
        }
    }
}
