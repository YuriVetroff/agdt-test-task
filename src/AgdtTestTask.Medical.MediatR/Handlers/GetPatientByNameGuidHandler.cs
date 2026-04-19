using AgdtTestTask.Medical.BusinessLogic.Abstracts;
using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using AgdtTestTask.Medical.MediatR.Queries;
using MediatR;

namespace AgdtTestTask.Medical.MediatR.Handlers
{
    internal sealed class GetPatientByNameGuidHandler(
        IPatientSearchingService service)
        : IRequestHandler<
            GetPatientByNameGuidQuery,
            PatientDTO>
    {
        public Task<PatientDTO> Handle(
            GetPatientByNameGuidQuery request,
            CancellationToken cancellationToken)
        {
            return service.GetByNameGuidAsync(
                request.Guid);
        }
    }
}
