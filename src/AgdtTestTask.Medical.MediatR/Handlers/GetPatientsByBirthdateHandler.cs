using AgdtTestTask.Medical.BusinessLogic.Abstracts;
using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using AgdtTestTask.Medical.MediatR.Queries;
using MediatR;

namespace AgdtTestTask.Medical.MediatR.Handlers
{
    internal sealed class GetPatientsByBirthdateHandler(
        IPatientSearchingService service)
        : IRequestHandler<
            GetPatientsByBirthdateQuery,
            IEnumerable<PatientDTO>>
    {
        public Task<IEnumerable<PatientDTO>> Handle(
            GetPatientsByBirthdateQuery request,
            CancellationToken cancellationToken)
        {
            return service.GetByBirthdateAsync(
                request.BirthdateParams);
        }
    }
}
