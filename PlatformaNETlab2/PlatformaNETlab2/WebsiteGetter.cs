using HtmlAgilityPack;

namespace PlatformaNETlab2
{
    public class WebsiteGetter
    {
        public async Task<IList<string>> GetWebsiteContent(string url)
        {
            var htmlDocuments = new List<string>();
            var client = new HttpClient();
            var mainPageDocumentResponse = await client.GetAsync(url);
            var mainPageDocumentString = await mainPageDocumentResponse.Content.ReadAsStringAsync();
            htmlDocuments.Add(mainPageDocumentString);

            var linkedUrls = GetLinkedSites(mainPageDocumentString).ToList();
            var tasks = new List<Task<string>>();
            var errors = new List<string>();

            await Task.Run(async () =>
            {
                for (int i = 0; i < linkedUrls.Count; i++)
                {
                    var linkedUrl = linkedUrls[i];
                    var urlToCall = linkedUrl.Contains("http") ? linkedUrl : url + linkedUrl;
                    HttpResponseMessage response = new HttpResponseMessage();
                    try
                    {
                        response = await client.GetAsync(urlToCall);
                    }
                    catch (Exception e)
                    {
                        errors.Add(urlToCall + ": error while reading.");
                    }
                    finally
                    {
                        tasks.Add(response.Content.ReadAsStringAsync());
                    }
                }
                
            });

            var responses = await Task.WhenAll(tasks.ToArray());
            errors.ForEach(err => Console.WriteLine(err));
            htmlDocuments.AddRange(responses);

            return htmlDocuments;
        }

        public IList<string> GetLinkedSites(string html)
        {
            var linkedSites = new List<string>();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            foreach (var link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                linkedSites.Add(link.GetAttributeValue("href", string.Empty)); 
            }
            return linkedSites;
        }
    }
}
