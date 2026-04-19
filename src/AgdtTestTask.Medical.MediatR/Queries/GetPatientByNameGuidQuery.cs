using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using MediatR;

namespace AgdtTestTask.Medical.MediatR.Queries
{
    public record GetPatientByNameGuidQuery(Guid Guid)
        : IRequest<PatientDTO>;
}
