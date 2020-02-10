using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace FogBugzApi
{
    internal class HttpClientHelper
    {
        private readonly HttpClient _client = new HttpClient();
        private string _apiToken;
        private string _urlString;

        internal string Token { get => _apiToken; }

        internal void initHttp(string url, string api)
        {
            _urlString = url;
            _apiToken = api;
        }

        internal string sendPost(string uri, FormUrlEncodedContent content)
        {
            var post = _client.PostAsync(uri, content);
            post.Wait();
            var htmlString = post.Result.Content.ReadAsStringAsync();
            htmlString.Wait();
            return (htmlString.Result ?? "");
        }

        /// <summary>
        /// Performs initial Post on given fogbugz uri. Returns html response string, "" if null"
        /// </summary>
        /// <param name="uriCommand"></param>
        /// <returns></returns>
        internal string postWithoutContent(string uriCommand)
        {
            //Use empty content
            var content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {" "," "}
                });
            return sendPost(uriCommand, content);
        }

        internal void checkResponseForErrorCode(XElement xml)
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

        /// <summary>
        /// Uses existing Url and Token to POST with the command in cmd= and returns an xml response. Raises appropriate exception if the response contains an error tag.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal XElement postAndGetXml(string command)
        {
            var post = postWithoutContent($"{_urlString}cmd={command}&token={_apiToken}");
            var xml = XElement.Parse(post);
            checkResponseForErrorCode(xml);
            return xml;
        }
    }
}
