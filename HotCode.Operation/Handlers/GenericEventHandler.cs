using System.Threading.Tasks;
using HotCode.Operation.Services;
using HotCode.System.Messaging;
using HotCode.System.Messaging.interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HotCode.Operation.Handlers
{
    [ServiceLocator.Service(ServiceLifetime.Scoped)]
    public class GenericEventHandler<T> : IEventHandler<T> where T : class, IEvent
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IOperationPublisher _operationPublisher;

        public GenericEventHandler(IOperationRepository operationRepository, IOperationPublisher operationPublisher)
        {
            _operationRepository = operationRepository;
            _operationPublisher = operationPublisher;
        }

        public async Task HandleAsync(T @event, CorrelationContext context)
        {
            switch (@event)
            {
                case IRejectedEvent rejectedEvent:
                {
                    await _operationRepository.SetAsync(context.Id, context.UserId, context.Name,
                        OperationState.Rejected, rejectedEvent.Code, rejectedEvent.Reason);
                    await _operationPublisher.RejectAsync(context, rejectedEvent.Code, rejectedEvent.Reason);
                    return;
                }
                case IEvent _:
                {
                    await _operationRepository.SetAsync(context.Id, context.UserId, context.Name,
                        OperationState.Completed);
                    await _operationPublisher.CompleteAsync(context);
                    return;
                }
            }
        }
    }
}