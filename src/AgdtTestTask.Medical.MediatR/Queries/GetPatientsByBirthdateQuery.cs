using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using MediatR;

namespace AgdtTestTask.Medical.MediatR.Queries
{
    public record GetPatientsByBirthdateQuery(IEnumerable<string> BirthdateParams) 
        : IRequest<IEnumerable<PatientDTO>>;
}
