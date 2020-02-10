using System.Text;

namespace FogBugzApi
{
    public class CaseManager
    {
        private HttpClientHelper _httpClient;
        internal CaseManager(HttpClientHelper httpClient)
        {
            _httpClient = httpClient;
        }

        public void EditCase(Case caseToEdit)
        {
            if (caseToEdit.Changeset == null) return;
            var sbChanges = new StringBuilder();
            foreach (var prop in caseToEdit.Changeset)
            {
                //TODO: create string to edit properties. User httpClient to update case
            }
        }
    }
}