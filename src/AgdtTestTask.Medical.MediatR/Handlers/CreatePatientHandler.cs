using AgdtTestTask.Medical.BusinessLogic.Abstracts;
using AgdtTestTask.Medical.MediatR.Commands;
using MediatR;

namespace AgdtTestTask.Medical.MediatR.Handlers
{
    internal sealed class CreatePatientHandler(
        IPatientModifyingService service)
        : IRequestHandler<CreatePatientCommand>
    {
        public Task Handle(
            CreatePatientCommand request,
            CancellationToken cancellationToken)
        {
            return service.CreatePatientAsync(
                request.Patient);
        }
    }
}
