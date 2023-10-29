using System.Net;
using Newtonsoft.Json;

namespace Lab_06
{
    class Programm
    {
        static void Main()
        {
            int count = 0;
            OpenWeather[] openWeathers = new OpenWeather[50];
            var linq = openWeathers;
            while (count < 50)
            {
                Random rand = new Random();
                double lat = rand.NextDouble() * rand.Next(-90, 90);
                double lon = rand.NextDouble() * rand.Next(-180, 180);
                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=7014cad18f46440ad10609dba5cc3cb4&units=metric";
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                string response;
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                    OpenWeather openWeather = JsonConvert.DeserializeObject<OpenWeather>(response);
                    if (openWeather.Sys.Country != "" && openWeather.Name != "")
                    {
                        openWeathers[count] = openWeather;
                        count++;
                    }
                }
                //httpResponse.Close();
            }
            var linq1 = openWeathers.FirstOrDefault(i => i.Weather[0].Description == "clear sky");
            var linq2 = openWeathers.FirstOrDefault(i => i.Weather[0].Description == "rain");
            var linq3 = openWeathers.FirstOrDefault(i => i.Weather[0].Description == "few clouds");
            var linq4 = openWeathers.MaxBy(i => i.Main.Temp);
            var linq5 = openWeathers.DistinctBy(i => i.Sys.Country);

            Print(linq);

            Console.WriteLine("---------------------------------------------------------");

            Print(linq4); //max temp

            avg_tmp(linq);

            Console.WriteLine(linq5.Count()); //разные страны

            Print(linq1); //"clear sky"
            Print(linq2); //"rain"
            Print(linq3); //"few clouds"

        }
        public static void Print(OpenWeather list)
        {
                if (list!=null)Console.WriteLine($"In {list.Sys.Country}, {list.Name} temp is {list.Main.Temp}°C and feel like {list.Main.Feels_like}°C. There {list.Weather[0].Name}, {list.Weather[0].Description}." +
                    $"");          
        }
        public static void Print(OpenWeather[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine($"In {list[i].Sys.Country}, {list[i].Name} temp is {list[i].Main.Temp}°C and feel like {list[i].Main.Feels_like}°C. There {list[i].Weather[0].Name}, {list[i].Weather[0].Description}." +
                    $"");
            }
        }
        public static void avg_tmp(OpenWeather[] list)
        {
            double tmp = 0;
            for (int i = 0;i < list.Length; i++) { tmp += list[i].Main.Temp; }
            tmp = tmp / list.Length;
            Console.WriteLine(tmp);
        }
    }
}