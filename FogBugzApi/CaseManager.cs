using System.Text;

namespace FogBugzApi
{
    public class CaseManager : ICaseManager
    {
        private HttpClientHelper _httpClient;
        internal CaseManager(HttpClientHelper httpClient)
        {
            _httpClient = httpClient;
        }

        /*
         * ===========NOTE FOR IMPLEMENTATION====================
         *  The API documentation for editing cases can be found here:
         *  https://support.fogbugz.com/hc/en-us/articles/360011343413-FogBugz-XML-API-Version-8#Sample_XML_Payloads
         *  The commands for resolve,close,email,reply and forward are all
         *  different versions of edit. I will not include these for now and
         *  let any application implementing this take care of it.
         */

        //TODO: Add resolve, close, email, reply and forward

        public void NewCase(Case caseWithChanges)
        {
            if (caseWithChanges.Changeset == null) return;
            var properties = createPropertyChangesetString(caseWithChanges);
            _httpClient.postAndGetXml("new" + properties);
        }

        public void EditCase(Case caseWithChanges)
        {
            if (caseWithChanges.Changeset == null) return;
            var properties = createPropertyChangesetString(caseWithChanges);
            _httpClient.postAndGetXml("edit" + properties);
        }

        public void AssignCase(Case caseWithChanges)
        {
            if (caseWithChanges.Changeset == null) return;
            var properties = createPropertyChangesetString(caseWithChanges);
            _httpClient.postAndGetXml("assign" + properties);
        }

        public void ReactivateCase(Case caseWithChanges)
        {
            if (caseWithChanges.Changeset == null) return;
            var properties = createPropertyChangesetString(caseWithChanges);
            _httpClient.postAndGetXml("reactivate" + properties);
        }

        public void ReopenCase(Case caseWithChanges)
        {
            if (caseWithChanges.Changeset == null) return;
            var properties = createPropertyChangesetString(caseWithChanges);
            _httpClient.postAndGetXml("reopen" + properties);
        }

        private string createPropertyChangesetString(Case caseWithChanges)
        {
            var sbChanges = new StringBuilder();
            foreach (var prop in caseWithChanges.Changeset)
            {
                sbChanges.Append(prop);
            }
            return sbChanges.ToString();
        }
    }
}