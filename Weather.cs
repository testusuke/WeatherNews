using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


namespace WeatherNews
{
    /// <summary>
    /// get 8 days weather news api
    /// Copyright.
    /// Author: testusuke
    /// github: https://github.com/testusuke
    /// Date: 2020/09/25
    /// </summary>
    public class Weather
    {
        //  API-Key
        String apiKey;

        //  Base URL
        const String dayBaseUrl = "http://api.openweathermap.org/data/2.5/weather?id=";
        const String dayOption = "&units=metric&appid=";
        const String weekBaseUrl = "https://api.openweathermap.org/data/2.5/onecall?";
        const String weekLocationLat = "lat=";
        const String weekLocationLon = "&lon=";
        const String weekOption = "&units=metric&exclude=minutely,hourly&appid=";

        public Weather(String apiKey)
        {
            this.apiKey = apiKey;
        }

        public Dictionary<String,String> getTodayWeather(String cityId)
        {
            String url = dayBaseUrl + cityId + dayOption + apiKey;
            //  request
            var stream = requestJson(url);
            //  create reader
            StreamReader reader = new StreamReader(stream);

            var obj_from_json = JObject.Parse(reader.ReadToEnd());
            var sum = obj_from_json["weather"][0]["main"];
            var des = obj_from_json["weather"][0]["description"];
            var icon = obj_from_json["weather"][0]["icon"];
            var temp = obj_from_json["main"]["temp"];
            var max_temp = obj_from_json["main"]["temp_max"];
            var min_temp = obj_from_json["main"]["temp_min"];
            var hum = obj_from_json["main"]["humidity"];
            var wind = obj_from_json["wind"]["speed"];
            var clouds = obj_from_json["clouds"]["all"];
            var city = obj_from_json["name"];

            //  add values to map
            var map = new Dictionary<string, string>()
            {
                {"wether", (String)sum},
                {"description", (String)des},
                {"wether_icon",(String)icon},
                {"temp", (String) temp},
                {"max_temp", (String) max_temp},
                {"min_temp", (String) min_temp},
                {"hum", (String) hum},
                {"wind", (String) wind},
                {"clouds", (String) clouds},
                {"city", (String) city}
            };

            return map;
        }

        public List<Dictionary<String,String>> getWeekWeather(String lat,String lon)
        {
            String url = weekBaseUrl + weekLocationLat + lat + weekLocationLon + lon + weekOption + apiKey;
            //  request
            var stream = requestJson(url);
            //  create reader
            StreamReader reader = new StreamReader(stream);
            var obj = JObject.Parse(reader.ReadToEnd());
            //  list of weather map]
            var list = new List<Dictionary<String, String>>();

            //  loop 0~7
            for (int i = 0; i <= 7; i++)
            {
                //  weather
                var sum = obj["daily"][i]["weather"][0]["main"];
                var des = obj["daily"][i]["weather"][0]["description"];
                var icon = obj["daily"][i]["weather"][0]["icon"];
                //  temp
                var temp = obj["daily"][i]["temp"]["day"];
                var temp_min = obj["daily"][i]["temp"]["min"];
                var temp_max = obj["daily"][i]["temp"]["max"];
                var temp_night = obj["daily"][i]["temp"]["night"];
                var temp_eve = obj["daily"][i]["temp"]["eve"];
                var temp_morn = obj["daily"][i]["temp"]["morn"];
                //  humidit
                var humidity = obj["daily"][i]["humidity"];
                //  clouds
                var clouds = obj["daily"][i]["clouds"];
                //  wind speed
                var wind_speed = obj["daily"][i]["wind_speed"];
                //  P.O.P(probability of precipitation)
                var pop = obj["daily"][i]["pop"];

                //  create map
                var map = new Dictionary<string, string>()
                {
                    {"wether", (String)sum},
                    {"description", (String)des},
                    {"wether_icon",(String)icon},
                    {"temp", (String) temp},
                    {"temp_min", (String) temp_min},
                    {"temp_max", (String) temp_max},
                    {"temp_night", (String) temp_night},
                    {"temp_eve", (String) temp_eve},
                    {"temp_morn", (String) temp_morn},
                    {"hum", (String) humidity},
                    {"wind", (String) wind_speed},
                    {"clouds", (String) clouds},
                    {"pop", (String) pop}
                };
                //  add list
                list.Add(map);
            }

            return list;
        }

        private Stream requestJson(String url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            Stream response_stream = webRequest.GetResponse().GetResponseStream();

            return response_stream;
        }
    }
}
