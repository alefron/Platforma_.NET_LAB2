using HtmlAgilityPack;
using System.Xml;

namespace PlatformaNETlab2
{
    public class WebsiteGetter
    {
        public async Task<IList<string>> GetWebsiteContent(string url)
        {
            var htmlContents = new List<string>();
            var client = new HttpClient();
            var content = await client.GetStringAsync(url);
            htmlContents.Add(content);

            var linkedUrls = GetLinkedSites(content).ToList();
            var tasks = new List<Task<string>>();

            linkedUrls.ForEach(linkedUrl =>
            {
                var urlToCall = linkedUrl.Contains("http") ? linkedUrl : url + linkedUrl;
                tasks.Add(client.GetStringAsync(urlToCall));
            });

            var responses = await Task.WhenAll(tasks.ToArray());
            htmlContents.AddRange(responses);

            return htmlContents;
        }

        public IList<string> GetLinkedSites(string html)
        {
            var linkedSites = new List<string>();
            var doc = new HtmlDocument();
            doc.Load(html);
            foreach (var link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                linkedSites.Add(link.GetAttributeValue("href", string.Empty)); 
            }
            return linkedSites;
        }
    }
}
