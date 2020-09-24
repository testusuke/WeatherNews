# Weather News API
## how to use?
##### Step 1 import library
You must import Newtonsoft.JSON library with NuGet.  
japanese page: http://masashieguchi.hatenablog.com/entry/2018/10/30/140129  
##### Step 2 add api class to your project
You can copy api class!
##### Step 3 get OpenWeatherMap api key.
You have to get api key.  
Go page and Sing up: https://openweathermap.org  
Next, Enable API key. It takes 20 minutes to 1 hour for the API key to become enable.  
```
//  Weather
String apiKey = "{your_api_key}";
Weather weather = new Weather(apiKey);
```
##### Step 4 create instance.
For use it, you create instance Weather.cs, and pass over api-key.  
##### Step 5 GET WEATHER NEWS!
Today Weather
```
var map = weather.getTodayWeather("city-id");
//  read
foreach(KeyValuePair<String,String> item in map)
{
  Console.WriteLine("[{0},{1}]", item.Key, item.Value);
}
```
Week Weather
```
var list = weather.getWeekWeather("Lat", "Lon");
//  read
int i = 0;
foreach(Dictionary<String,String> map in list){
  Console.WriteLine("{0} day", i);
  foreach(KeyValuePair<String, String> item in map)
  {
    Console.WriteLine("[{0},{1}]", item.Key, item.Value);
  }
  i++;
}
```
## LICENSE
unlicense  
copyright!!
