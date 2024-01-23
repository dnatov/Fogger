using Fogger.Interfaces;
using Fogger.Models;
using System.Text;

namespace Fogger.Services
{
    public class CaseManagerV8 : ICaseManager
    {
        private IHttpClient _httpClient;
        internal CaseManagerV8(IHttpClient httpClient)
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

        /// <summary>
        /// Uses the Case.Changset command strings to create a new case.
        /// </summary>
        /// <param name="caseWithChanges"></param>
        public void NewCase(Case caseWithChanges)
        {
            if (caseWithChanges.Changeset == null) return;
            var properties = createPropertyChangesetString(caseWithChanges);
            _httpClient.PostAndGetXml("new" + properties);
        }

        /// <summary>
        /// Uses the <see cref="Case.Changset"/> command strings to create a edit an existing case.
        /// </summary>
        /// <param name="caseWithChanges"></param>
        public void EditCase(Case caseWithChanges)
        {
            if (caseWithChanges.Changeset == null) return;
            var properties = createPropertyChangesetString(caseWithChanges);
            _httpClient.PostAndGetXml("edit" + properties);
        }

        /// <summary>
        /// Assigns an existing case with additional edits from Case.Changset command strings.
        /// </summary>
        /// <param name="caseWithChanges"></param>
        public void AssignCase(Case caseWithChanges)
        {
            if (caseWithChanges.Changeset == null) return;
            var properties = createPropertyChangesetString(caseWithChanges);
            _httpClient.PostAndGetXml("assign" + properties);
        }

        /// <summary>
        /// Reactivates an existing case with additional edits from Case.Changset command strings.
        /// </summary>
        /// <param name="caseWithChanges"></param>
        public void ReactivateCase(Case caseWithChanges)
        {
            if (caseWithChanges.Changeset == null) return;
            var properties = createPropertyChangesetString(caseWithChanges);
            _httpClient.PostAndGetXml("reactivate" + properties);
        }

        /// <summary>
        /// Reopens an existing case with additional edits from Case.Changset command strings.
        /// </summary>
        /// <param name="caseWithChanges"></param>
        public void ReopenCase(Case caseWithChanges)
        {
            if (caseWithChanges.Changeset == null) return;
            var properties = createPropertyChangesetString(caseWithChanges);
            _httpClient.PostAndGetXml("reopen" + properties);
        }

        /// <summary>
        /// Takes an input case and creates a single string combining all the commands to pass to the HTML Client
        /// </summary>
        /// <param name="caseWithChanges"></param>
        /// <returns></returns>
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