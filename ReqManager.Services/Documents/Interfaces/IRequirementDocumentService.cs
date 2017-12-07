namespace ReqManager.Services.Documents.Interfaces
{
    public interface IRequirementDocumentService
    {
        byte[] printRequirement(int RequirementID);
        byte[] printDocumentRequirementProject(int ProjectID, int RequirementTypeID);
    }
}
