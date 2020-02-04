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
        private static int _version;
        private static int _minVersion;
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
        public static string PostWithoutContent(string uri)
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
        /// Grabs the initial Url for all further calls to the API. Gets version info of the FogBugz install as well.
        /// </summary>
        /// <param name="uri"></param>
        public static void InitializeApi(string uri)
        {
            //Store main uri
            _mainUri = uri;

            //Initial Post
            var post = PostWithoutContent(uri + @"/api.xml");
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
            var post = PostWithoutContent($"{Url}cmd=logon&email={user}&password=&pass");

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
            Ambiguous
        }

        /// <summary>
        /// Checks if the logon session is still valid.
        /// </summary>
        /// <returns></returns>
        public static bool ValidateLogon()
        {
            var post = PostWithoutContent($"{Url}cmd=logon&token={_token}");

            //Store in XElement Obj
            var xml = XElement.Parse(post);

            //Parse HTML for token. FogBugz recommends using this token as long as you can in favor of a new login request.
            var token = (xml.Elements().Where(x => x.Name.LocalName == "token").FirstOrDefault().Value);

            //If an error happens then the logon is not valid
            if (xml.Elements().Where(x => x.Name.LocalName == "error").Any()) return false;

            //Logon is only valid if the token that is returned is the same as the token stored
            return token.Equals(_token);
        }
    }
}
