// See https://aka.ms/new-console-template for more information

using PlatformaNETlab2;
using System.Text;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var carService = new CarService();

carService.PitStopAsync();
Console.ReadKey();

carService.PitStop();
Console.ReadKey();

Console.WriteLine("Getting website content...");

var websiteContentGetter = new WebsiteGetter();
var content = await  websiteContentGetter.GetWebsiteContent("https://www.google.com/");

content.ToList().ForEach(doc => Console.WriteLine(doc));

Console.ReadKey();


