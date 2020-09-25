using Fogger.Models;

namespace Fogger.Interfaces
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