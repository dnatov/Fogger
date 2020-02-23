using System.Net.Http;
using System.Xml.Linq;

namespace FogBugzApi
{
    public interface IHttpClient
    {
        public string ApiToken { get; set; }
        public string UriString { get; set; }

        /// <summary>
        /// Uses existing Url and Token to POST with the command in cmd= and returns an xml response. Raises appropriate exception if the response contains an error tag.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public XElement PostAndGetXml(string command);

        /// <summary>
        /// Performs initial Post on given fogbugz uri. Returns html response string, "" if null"
        /// </summary>
        /// <param name="uriCommand"></param>
        /// <returns></returns>
        public string PostWithoutContent(string uriCommand);

        /// <summary>
        /// Checks the XElement response for the existance of an error code. Throws exception when it exists.
        /// </summary>
        /// <param name="xml"></param>
        public void CheckResponseForErrorCode(XElement xml);

        /// <summary>
        /// An HTTP request with content
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string SendPost(string uri, FormUrlEncodedContent content);
    }
}