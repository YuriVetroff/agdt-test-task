using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using MediatR;

namespace AgdtTestTask.Medical.MediatR.Commands
{
    public record UpdatePatientCommand(long PatientId, PatientDTO Patient)
        : IRequest;
}
