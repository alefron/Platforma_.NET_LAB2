// See https://aka.ms/new-console-template for more information

using PlatformaNETlab2;

var carService = new CarService();

//carService.PitStopAsync();
Console.ReadKey();

//carService.PitStop();
Console.ReadKey();

var websiteContentGetter = new WebsiteGetter();
var content = await  websiteContentGetter.GetWebsiteContent("https://www.google.com/");

Console.WriteLine(content);

Console.ReadKey();


