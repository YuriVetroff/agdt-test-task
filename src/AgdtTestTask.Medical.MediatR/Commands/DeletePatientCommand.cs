using MediatR;

namespace AgdtTestTask.Medical.MediatR.Commands
{
    public record DeletePatientCommand(long PatientId)
        : IRequest;
}
