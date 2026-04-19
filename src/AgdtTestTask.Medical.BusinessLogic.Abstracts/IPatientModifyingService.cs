using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;

namespace AgdtTestTask.Medical.BusinessLogic.Abstracts
{
    public interface IPatientModifyingService
    {
        Task CreatePatientAsync(PatientDTO dto);
        Task UpdatePatientAsync(long patientId, PatientDTO dto);
        Task DeletePatientAsync(long patientId);
    }
}
