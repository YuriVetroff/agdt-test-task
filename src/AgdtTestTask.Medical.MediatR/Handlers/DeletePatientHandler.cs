using AgdtTestTask.Medical.BusinessLogic.Abstracts;
using AgdtTestTask.Medical.MediatR.Commands;
using MediatR;

namespace AgdtTestTask.Medical.MediatR.Handlers
{
    internal sealed class DeletePatientHandler(
        IPatientModifyingService service)
        : IRequestHandler<DeletePatientCommand>
    {
        public Task Handle(
            DeletePatientCommand request,
            CancellationToken cancellationToken)
        {
            return service.DeletePatientAsync(
                request.PatientId);
        }
    }
}
