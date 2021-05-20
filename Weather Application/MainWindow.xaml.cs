using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Drawing;
using System.Xml;


namespace Weather_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string APPID = "542ffd081e67f4512b705f89d2a611b2";
        string cityName = "Islamabad";
        
        public MainWindow()
        {

            InitializeComponent();
            getWeather(cityName);
            getWeatherForcast(cityName);
         
        }

        void getWeather(string city)
        {
            try
            {
                using (WebClient web = new WebClient())
                {
                    string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric", city, APPID);
                    var json = web.DownloadString(url);
                    var result = JsonConvert.DeserializeObject<WeatherInfo.root>(json);
                    WeatherInfo.root output = result;

                    lb_CityName.Content = string.Format("{0}", output.name);
                    lb_Time.Content = string.Format("{0}", getDate(output.dt).TimeOfDay);
                    lb_CountryName.Content = string.Format("{0}", output.sys.country);
                    lb_CityTemp.Content = string.Format("{0}", output.main.temp);
                    lb_TempMax.Content = string.Format("{0}", output.main.temp_max);
                    lb_TempMin.Content = string.Format("{0}", output.main.temp_min);
                    lb_sunrisetime.Content = string.Format("{0}",date(output.sys.sunrise));
                    lb_sunsetTime.Content = string.Format("{0}", date(output.sys.sunset));
                    lb_WindSpeed.Content = string.Format("{0} m/s",output.wind.speed);
                    lb_windDegree.Content = string.Format("{0}",output.wind.deg);
                   
                  
                    var desc="" ;
                    foreach (var item in output.weather)
                    {
                         desc = item.main;
                        
                    }

                    

                   lb_WeatherMain.Content = string.Format("{0}",desc);
                   imageSetter(desc);
                    InitializeComponent();

                }
            }

            catch (Exception e)
            { 

                
            }
        
        }

        void getWeatherForcast(string city)
        {
            
            int day=5;
           try{

            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt={1}&APPID={2}", city,day ,APPID);
             using (WebClient web = new WebClient())
             {
                 var json = web.DownloadString(url);
                 var Object = JsonConvert.DeserializeObject<WeatherForcast>(json);
                 WeatherForcast forcast = Object;


                 for (int i = 1; i < day; i++)
                 {
                     string b = string.Format("{0}", getDate(forcast.list[i].dt).DayOfWeek);
                     lbox_Forcast.Items.Add(b);// return day in milli
                     lbox_Forcast.Items.Add(string.Format("         " + "{0}", forcast.list[i].weather[0].main));// weather condition
                     lbox_Forcast.Items.Add(string.Format("         " + "{0}", forcast.list[i].weather[0].description)); /// weather desc
                     lbox_Forcast.Items.Add(string.Format("         " + "{0}\u0080" + "C", forcast.list[i].temp.day));//weather temp
                     lbox_Forcast.Items.Add(string.Format("         " + "{0} km/h", forcast.list[i].speed)); // wind speed
                 }


                 
             }
            }
            catch(Exception e)
            {

                Console.WriteLine(e);
            }
        
        }

        DateTime getDate(double millisecound)
        {

            DateTime day = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisecound).ToLocalTime();

            return day;
        }

        public string date(double x)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(x);
            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds
                                    );
            return answer;
        }

        private void selectedFromMajorCity(object sender, MouseButtonEventArgs e)
        {
            string text = ((ListBoxItem)LB_MajorCities.SelectedItem).Content.ToString();
            getWeather(text);
            lbox_Forcast.Items.Clear();
            getWeatherForcast(text);
        }


        public void imageSetter(string desc)
        {

            if (desc == "Haze")
            {
                weatherImage.Source = new BitmapImage(new Uri(@"C:\Users\Usman\Downloads\Images\sunny.png", UriKind.RelativeOrAbsolute)); 
            }
            else if(desc =="Clear")
            {
                weatherImage.Source = new BitmapImage(new Uri(@"C:\Users\Usman\Downloads\Images\sun.png", UriKind.RelativeOrAbsolute)); 
            }
            else if (desc == "Rain")
            {
                weatherImage.Source = new BitmapImage(new Uri(@"C:\Users\Usman\Downloads\Images\rain.png", UriKind.RelativeOrAbsolute));
            }
            else if (desc == "Clouds")
            {
                weatherImage.Source = new BitmapImage(new Uri(@"C:\Users\Usman\Downloads\Images\cloud.png", UriKind.RelativeOrAbsolute));
            }
            else if (desc == "Fog")
            {
                weatherImage.Source = new BitmapImage(new Uri(@"C:\Users\Usman\Downloads\Images\fog.png", UriKind.RelativeOrAbsolute));
            }
            else if (desc == "Smoke")
            {
                weatherImage.Source = new BitmapImage(new Uri(@"C:\Users\Usman\Downloads\Images\smoke.png", UriKind.RelativeOrAbsolute));
            }
            else if (desc == "Drizzle")
            {
                weatherImage.Source = new BitmapImage(new Uri(@"C:\Users\Usman\Downloads\Images\rain.png", UriKind.RelativeOrAbsolute));
            }
            else if (desc == "Snow")
            {
                weatherImage.Source = new BitmapImage(new Uri(@"C:\Users\Usman\Downloads\Images\snowy.png", UriKind.RelativeOrAbsolute));
            }
            else 
            {
                weatherImage.Source = new BitmapImage(new Uri(@"C:\Users\Usman\Downloads\Images\sun.png", UriKind.RelativeOrAbsolute));
            }
        }
        private void Bt_getLocation_Click(object sender, RoutedEventArgs e)
        {
            string IP = "";

            string HostName = "";
            try
            {

            
                XmlDocument doc = new XmlDocument();
                doc.Load("http://www.freegeoip.net/xml");
                XmlNodeList nodeList = doc.GetElementsByTagName("City");
                IP = "" + nodeList[0].InnerText;
               
                InitializeComponent();
                getWeather(IP);
                getWeatherForcast(IP);
            }
            catch(Exception sd)
            { }

        }

        private void Bt_Search_Click(object sender, RoutedEventArgs e)
        {
            if(searchText.Text==""){

            }
            else{
                LoadJson(searchText.Text);
            }
        }

        private void LoadJson(string city)
        {
            using (StreamReader r = new StreamReader("city.json"))
            {
                Console.WriteLine(">>>>"+searchText.Text);
                string json = r.ReadToEnd();
                List<City> items = JsonConvert.DeserializeObject<List<City>>(json);
                try { 
                foreach (var list in items)
                {
                    Console.WriteLine(list.name);

                    if (city.Equals(list.name))
                    {
                        Console.WriteLine(city);
                        getWeather(city);
                        lbox_Forcast.Items.Clear();
                        getWeatherForcast(city);
                        InitializeComponent();
                        break;
                    }
                }

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
                }
            
        }

        private void bt_AddToFavourite_Click(object sender, RoutedEventArgs e)
        {
            string city = lb_CityName.Content.ToString();
            while (!lbox_Favourite.Items.Contains(city))
            {
                lbox_Favourite.Items.Add(city);
                break;
            }
           
             InitializeComponent();

         }

            
            
           
           
        

        private void Bt_RemoveFavourite_Click(object sender, RoutedEventArgs e)
        {
            string city = lb_CityName.Content.ToString();

            if (lbox_Favourite.SelectedIndex!=-1)
            {
             
                lbox_Favourite.Items.RemoveAt(lbox_Favourite.SelectedIndex);
                
            }
            else 
            {
                MessageBox.Show("Please select a favourite Item you want to delete", "Warning",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
            InitializeComponent();
        }

        private void selectFromFavorite(object sender, MouseButtonEventArgs e)
        {
            string text = lbox_Favourite.SelectedItem.ToString();
            getWeather(text);
            lbox_Forcast.Items.Clear();
            getWeatherForcast(text);

        }

        private void bt_CelsiusToFahrenheit_Click(object sender, RoutedEventArgs e)
        {

            double tempMain = Math.Round(ConvertTemp.ConvertCelsiusToFahrenheit(double.Parse(lb_CityTemp.Content.ToString())),2);
            double maxTemp =  Math.Round(ConvertTemp.ConvertCelsiusToFahrenheit(double.Parse(lb_TempMax.Content.ToString())),2);
            double minTemp = Math.Round(ConvertTemp.ConvertCelsiusToFahrenheit(double.Parse(lb_TempMin.Content.ToString())), 2);
            lb_CityTemp.Content = tempMain.ToString();
            lb_TempMax.Content = maxTemp.ToString();
            lb_TempMin.Content = minTemp.ToString();
            Bt_FahrenheitToCelsius.Visibility = Visibility.Visible;
            bt_CelsiusToFahrenheit.Visibility = Visibility.Hidden;
        }

        private void Bt_FahrenheitToCelsius_Click(object sender, RoutedEventArgs e)
        {
            double tempMain = Math.Round( ConvertTemp.ConvertFahrenheitToCelsius(double.Parse(lb_CityTemp.Content.ToString())),2);
            double maxTemp =  Math.Round(ConvertTemp.ConvertFahrenheitToCelsius(double.Parse(lb_TempMax.Content.ToString())),2);
            double minTemp = Math.Round(ConvertTemp.ConvertFahrenheitToCelsius(double.Parse(lb_TempMin.Content.ToString())), 2);
            
            lb_CityTemp.Content = tempMain.ToString();
            lb_TempMax.Content = maxTemp.ToString();
            lb_TempMin.Content = minTemp.ToString();
            Bt_FahrenheitToCelsius.Visibility = Visibility.Hidden;
            bt_CelsiusToFahrenheit.Visibility = Visibility.Visible;
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }


        
    }
    static class ConvertTemp
    {
        public static double ConvertCelsiusToFahrenheit(double c)
        {
            return ((9.0 / 5.0) * c) + 32;
        }

        public static double ConvertFahrenheitToCelsius(double f)
        {
            return (5.0 / 9.0) * (f - 32);
        }
    }
}
