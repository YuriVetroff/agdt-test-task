using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using MediatR;

namespace AgdtTestTask.Medical.MediatR.Commands
{
    public record CreatePatientCommand(PatientDTO Patient)
        : IRequest;
}
