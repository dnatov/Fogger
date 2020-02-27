using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml;
using System.Linq;
using System.Text;

namespace FogBugzApi
{
    public class FogBugzApiWrapper
    {
        private int _version = -1;
        private int _minVersion = -1;
        private Filter _previousFilter;

        internal IHttpClient HttpClientHelper;
        public ICaseManager CaseManager;

        public int Version { get => _version; set => _version = value; }
        public int MinVersion { get => _minVersion; set => _minVersion = value; }

        /// <summary>
        /// Grabs the initial Url for all further calls to the API. Gets version info of the FogBugz and initializes the Http Client
        /// </summary>
        /// <param name="uri"></param>
        private void initializeApi(string uri)
        {
            HttpClientHelper = HttpClientFactory.CreateHttpClient();

            //Initial Post
            var post = HttpClientHelper.PostWithoutContent(uri + @"/api.xml");

            //Store in XElement Obj
            var xml = XElement.Parse(post);
            HttpClientHelper.CheckResponseForErrorCode(xml);

            //Parse HTML
            _version = Int32.Parse(xml.Elements().Where(x => x.Name.LocalName == "version").FirstOrDefault().Value);
            _minVersion = Int32.Parse(xml.Elements().Where(x => x.Name.LocalName == "minversion").FirstOrDefault().Value);
            HttpClientHelper.UriString = uri + '/' + xml.Elements().Where(x => x.Name.LocalName == "url").FirstOrDefault().Value;

            //Create version dependent Case Manager
            CaseManager = CaseManagerFactory.CreateCaseManager(_version, _minVersion, HttpClientHelper);
        }

        /// <summary>
        /// Performs a logon operation using the Email and Password of the user. Retreives the api token.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        private string logon(string user,string pass)
        {
            //POST using other private method since this assume token is undefined
            var post = HttpClientHelper.PostWithoutContent($"{HttpClientHelper.UriString}cmd=logon&email={user}&password={pass}");

            //Store in XElement Obj
            var xml = XElement.Parse(post);
            HttpClientHelper.CheckResponseForErrorCode(xml);

            //Parse HTML for token. FogBugz recommends using this token as long as you can in favor of a new login request.
            return (xml.Elements().Where(x => x.Name.LocalName == "token").FirstOrDefault().Value);
        }

        public FogBugzApiWrapper(string uri, string username, string password)
        {
            initializeApi(uri);
            var token = logon(username, password);
            HttpClientHelper.ApiToken = token;
        }

        public FogBugzApiWrapper(string uri, string token)
        {
            initializeApi(uri);
            HttpClientHelper.ApiToken = token;
        }

        /// <summary>
        /// Checks if the logon session is still valid.
        /// </summary>
        /// <returns></returns>
        public bool ValidateLogon()
        {
            var xml = HttpClientHelper.PostAndGetXml("logon");

            //Parse HTML for token. FogBugz recommends using this token as long as you can in favor of a new login request.
            var token = (xml.Elements().Where(x => x.Name.LocalName == "token").FirstOrDefault().Value);

            //Logon is only valid if the token that is returned is the same as the token stored
            return token.Equals(HttpClientHelper.ApiToken);
        }

        public bool Logoff()
        {
            HttpClientHelper.PostAndGetXml("logoff");
            _version = -1;
            _minVersion = -1;
            _previousFilter = null;
            HttpClientHelper = null;

            return true;
        }

        /// <summary>
        /// Gets all the filters from the server
        /// </summary>
        /// <returns></returns>
        public List<Filter> GetFilters()
        {
            //get the xml response listing all filters
            var xmlResponse = HttpClientHelper.PostAndGetXml("listFilters");

            //Grab filters element
            var xmlFilters = xmlResponse.Element(XName.Get("filters", xmlResponse.GetDefaultNamespace().ToString()));

            //Populate list of Filters and return
            var filters = new List<Filter>();
            foreach(var fil in xmlFilters.Elements())
            {
                var type = Filter.StringToFilterType(fil.Attribute("type").Value);
                var sFilter = fil.Attribute("sFilter").Value;
                var current = fil?.Attribute("status")?.Value ?? "";
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
        public void SetCurrentFilter(Filter newFilter)
        {
            _previousFilter?.SetNotCurrent();

            //Set current filter, reponse should be empty
            HttpClientHelper.PostAndGetXml($"setCurrentFilter&sFilter={newFilter.sFilter}");

            newFilter.SetCurrent();

            _previousFilter = newFilter;
        }

        public List<Case> SearchCurrentFilter()
        {
            return Search("");
        }

        /// <summary>
        /// Searches for the given query, returns case information using default column names and 50 cases
        /// </summary>
        /// <param name="query"></param>
        public List<Case> Search(string query)
        {
            //Default Search query columns for now
            var columns = new List<string>()
            {
                "sArea", //Area
                "ixBug", //Case #
                "sCategory", //Category
                "ixPriority", //Priority integer
                "sPriority", //Priority name
                "sProject", //Project name
                "sStatus", //Status
                "sTitle" //Title
            };
            return Search(query, columns);
        }
        /// <summary>
        /// Searches for cases using the query term and returns case information using columnNames up to 50 cases
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnNames"></param>
        public List<Case> Search(string query, IList<string> columnNames) 
        {
            return Search(query, columnNames, 50);
        }
        /// <summary>
        /// Searches for the given query, returns the column information for a desinated case amount
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columnNames"></param>
        /// <param name="max"></param>
        public List<Case> Search(string query, IList<string> columnNames, int max)
        {
            var cases = new List<Case>();
            var sbColumns = new StringBuilder();
            foreach (var col in columnNames)
            {
                sbColumns.Append(col + ",");
            }
            sbColumns.Remove(sbColumns.Length - 1, 1);
            var xml = HttpClientHelper.PostAndGetXml($"search&q={query}&cols={sbColumns.ToString()}&max={max}");
            Console.WriteLine(xml.ToString());
            foreach(var caseXml in xml.Elements().Elements().Where(c=>c.Name.LocalName == "case"))
            {
                cases.Add(new Case(caseXml));
            }
            return cases;
        }

    }
}
