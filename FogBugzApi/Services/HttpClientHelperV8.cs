using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace FogBugzApi
{
    public class HttpClientHelper : IHttpClient
    {
        private readonly HttpClient _client = new HttpClient();
        private string _apiToken;
        private string _uriString;

        public string ApiToken { get => _apiToken; set => _apiToken = value; }
        public string UriString { get => _uriString; set => _uriString = value; }

        public string SendPost(string uri, FormUrlEncodedContent content)
        {
            var post = _client.PostAsync(uri, content);
            post.Wait();
            var htmlString = post.Result.Content.ReadAsStringAsync();
            htmlString.Wait();
            return (htmlString.Result ?? "");
        }

        
        public string PostWithoutContent(string uriCommand)
        {
            //Use empty content
            var content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {" "," "}
                });
            return SendPost(uriCommand, content);
        }

        public void CheckResponseForErrorCode(XElement xml)
        {
            //Check for error tag and its attribute
            var errorElement = xml.Elements().Where(x => x.Name.LocalName == "error").FirstOrDefault();
            if (errorElement?.HasAttributes ?? false)
            {
                errorElement = (xml.Elements().Where(x => x.Name.LocalName == "error").FirstOrDefault());
                int result;
                if (Int32.TryParse(errorElement.Attribute("code").Value, out result))
                {
                    var errorString = errorElement.Value;
                    throw new FogbugzException(result, errorString);
                }
            }
        }

        public XElement PostAndGetXml(string command)
        {
            var post = PostWithoutContent($"{_uriString}cmd={command}&token={_apiToken}");
            var xml = XElement.Parse(post);
            CheckResponseForErrorCode(xml);
            return xml;
        }
    }
}
