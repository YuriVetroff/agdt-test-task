using AgdtTestTask.Medical.BusinessLogic.Abstracts;
using AgdtTestTask.Medical.MediatR.Commands;
using MediatR;

namespace AgdtTestTask.Medical.MediatR.Handlers
{
    internal sealed class UpdatePatientHandler(
        IPatientModifyingService service)
        : IRequestHandler<UpdatePatientCommand>
    {
        public Task Handle(
            UpdatePatientCommand request,
            CancellationToken cancellationToken)
        {
            return service.UpdatePatientAsync(
                request.PatientId,
                request.Patient);
        }
    }
}
