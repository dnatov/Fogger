namespace FogBugzApi
{
    public interface ICaseManager
    {
        void NewCase(Case caseWithChanges);
        void EditCase(Case caseWithChanges);
        void AssignCase(Case caseWithChanges);
        void ReactivateCase(Case caseWithChanges);
        void ReopenCase(Case caseWithChanges);
    }
}