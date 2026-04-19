using AgdtTestTask.Core.Common.Extensions;
using AgdtTestTask.Core.DataAccess.Abstracts;
using AgdtTestTask.Core.DataAccess.Abstracts.Extensions;
using AgdtTestTask.Medical.BusinessLogic.Abstracts;
using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using AgdtTestTask.Medical.Entities;
using AutoMapper;
using System.Linq.Expressions;

namespace AgdtTestTask.Medical.BusinessLogic
{
    internal sealed class PatientModifyingService(
        IRepository<Patient> patientRepository,
        IMapper mapper)
        : IPatientModifyingService
    {
        public async Task CreatePatientAsync(
            PatientDTO dto)
        {
            await ValidateGuidAsync(dto);

            var newPatient = mapper.Map<Patient>(dto);
            await patientRepository.AddAsync(newPatient);
        }

        public Task DeletePatientAsync(
            long patientId)
        {
            return patientRepository.DeleteByIdAsync(patientId);
        }

        public async Task UpdatePatientAsync(
            long patientId,
            PatientDTO dto)
        {
            await ValidateGuidAsync(dto, patientId);

            var existingPatient = await patientRepository.GetRequiredAsync(patientId);
            mapper.Map(dto, existingPatient);
            await patientRepository.UpdateAsync(existingPatient);
        }

        private async Task ValidateGuidAsync(
            PatientDTO dto,
            long? patientId = null)
        {
            var guid = dto?.Name?.Id;
            if (guid.HasValue)
            {
                Expression<Func<Patient, bool>> predicate = x =>
                    x.Name.Id == guid.Value;
                if (patientId.HasValue)
                {
                    predicate = predicate.AndAlso(x => x.Id != patientId.Value);
                }

                var patientWithSameGuid = await patientRepository
                    .FirstOrDefaultAsync(predicate);
                if (patientWithSameGuid != null)
                {
                    throw new ArgumentException(
                        $"The GUID {guid} is already used");
                }
            }
        }
    }
}
