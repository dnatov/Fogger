using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml;
using System.Linq;

namespace FogBugzApi
{
    public class FogBugzApiWrapper
    {
        private static readonly HttpClient _client = new HttpClient();
        private static int _version = -1;
        private static int _minVersion = -1;
        private static string _url;
        private static string _mainUri;
        private static string _token;

        public static int Version { get => _version; set => _version = value; }
        public static int MinVersion { get => _minVersion; set => _minVersion = value; }
        public static string Url { get => _mainUri + '/' + _url; set => _url = value; }

        private static string sendPost(string uri, FormUrlEncodedContent content)
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
        /// <param name="uri"></param>
        /// <returns></returns>
        private static string postWithoutContent(string uri)
        {
            //Use empty content
            var content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {" "," "}
                });
            return sendPost(uri, content);
        }

        /// <summary>
        /// Uses existing Url and Token to POST with the command in cmd= and returns an xml response
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private static XElement postAndGetXml(string command)
        {
            var post = postWithoutContent($"{Url}cmd={command}&token={_token}");

            return XElement.Parse(post);
        }

        /// <summary>
        /// Grabs the initial Url for all further calls to the API. Gets version info of the FogBugz install as well.
        /// </summary>
        /// <param name="uri"></param>
        public static void InitializeApi(string uri)
        {
            //Store main uri
            _mainUri = uri;

            //Initial Post
            var post = postWithoutContent(uri + @"/api.xml");
            //Store in XElement Obj
            var xml = XElement.Parse(post);
            //Parse HTML

            _version = Int32.Parse(xml.Elements().Where(x => x.Name.LocalName == "version").FirstOrDefault().Value);
            _minVersion = Int32.Parse(xml.Elements().Where(x => x.Name.LocalName == "minversion").FirstOrDefault().Value);
            _url = xml.Elements().Where(x => x.Name.LocalName == "url").FirstOrDefault().Value;
        }

        /// <summary>
        /// Performs a logon operation using the Email and Password of the user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns>Error Code</returns>
        public static int Logon(string user,string pass)
        {
            //POST using other private method since this assume token is undefined
            var post = postWithoutContent($"{Url}cmd=logon&email={user}&password=&pass");

            //Store in XElement Obj
            var xml = XElement.Parse(post);

            //Parse HTML for token. FogBugz recommends using this token as long as you can in favor of a new login request.
            _token = (xml.Elements().Where(x => x.Name.LocalName == "token").FirstOrDefault().Value);

            //Check errors
            if (xml.HasAttributes)
            {
                var errorElement = (xml.Elements().Where(x => x.Name.LocalName == "error").FirstOrDefault());
                int result = -1;
                Int32.TryParse(errorElement.Attribute("code").Value, out result);
                return result;
            }
            else
            {
                return 0;
            }
        }

        public enum LogonErrorCodes
        {
            Unknown,
            EmailPasswordMismatch,
            TwoFA,
            InvalidToken,
            Ambiguous
        }

        /// <summary>
        /// Checks if the logon session is still valid.
        /// </summary>
        /// <returns></returns>
        public static bool ValidateLogon()
        {
            //Store in XElement Obj
            var xml = postAndGetXml("logon");

            //Parse HTML for token. FogBugz recommends using this token as long as you can in favor of a new login request.
            var token = (xml.Elements().Where(x => x.Name.LocalName == "token").FirstOrDefault().Value);

            //If an error happens then the logon is not valid
            if (xml.Elements().Where(x => x.Name.LocalName == "error").Any()) return false;

            //Logon is only valid if the token that is returned is the same as the token stored
            return token.Equals(_token);
        }

        public static bool Logoff()
        {
            var xmlResponse = postAndGetXml("logoff");
            _token = null;
            _url = null;
            _mainUri = null;
            _version = -1;
            _minVersion = -1;

            return true;
        }

        /// <summary>
        /// Gets all the filters from the server
        /// </summary>
        /// <returns></returns>
        public static List<Filter> GetFilters()
        {
            //get the xml response listing all filters
            var xmlResponse = postAndGetXml("listFilters");

            //Grab filters element
            var xmlFilters = xmlResponse.Element(XName.Get("filters", xmlResponse.GetDefaultNamespace().ToString()));

            //Populate list of Filters and return
            var filters = new List<Filter>();
            foreach(var fil in xmlFilters.Elements())
            {
                var type = Filter.StringToFilterType(fil.Attribute("type").Value);
                var sFilter = fil.Attribute("sFilter").Value;
                var current = fil.Attribute("status").Value ?? "";
                var description = fil.Value;

                //If the filter is currently selected then initialize it as such
                if (current == "current")
                    filters.Add(new Filter(type, sFilter, description, true));
                else
                    filters.Add(new Filter(type, sFilter, description));
            }

            return filters;
        }

        /// <summary>
        /// Changes the current filter
        /// </summary>
        /// <param name="previousFilter"></param>
        /// <param name="newFilter"></param>
        public static void ChangeCurrentFilter(Filter previousFilter, Filter newFilter)
        {
            previousFilter.SetNotCurrent();

            //Set current filter, reponse should be empty
            var xmlResponse = postAndGetXml($"setCurrentfilter&sFilter={newFilter.sFilter}");

            newFilter.SetCurrent();
        }
    }
}
