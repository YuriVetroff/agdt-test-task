using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;

namespace AgdtTestTask.Medical.BusinessLogic.Abstracts
{
    public interface IPatientSearchingService
    {
        Task<PatientDTO> GetByNameGuidAsync(
            Guid guid);

        Task<IEnumerable<PatientDTO>> GetByBirthdateAsync(
            IEnumerable<string> birthdateParams);
    }
}
