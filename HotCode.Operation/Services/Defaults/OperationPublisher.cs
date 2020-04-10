using System.Threading.Tasks;
using HotCode.Operation.Messages.Operations.Events;
using HotCode.System.Messaging;
using HotCode.System.Messaging.interfaces;
using Microsoft.Extensions.DependencyInjection;
using ServiceLocator;

namespace HotCode.Operation.Services.Defaults
{
    [Service(ServiceLifetime.Scoped)]
    public class OperationPublisher : IOperationPublisher
    {
        private readonly IBusPublisher _busPublisher;

        public OperationPublisher(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task PendingAsync(CorrelationContext context)
            => await _busPublisher.PublishAsync(new OperationPending(context.Id,
                context.UserId, context.Name), context);

        public async Task CompleteAsync(CorrelationContext context)
            => await _busPublisher.PublishAsync(new OperationCompleted(context.Id,
                context.UserId, context.Name), context);

        public async Task RejectAsync(CorrelationContext context, string code, string message)
            => await _busPublisher.PublishAsync(new OperationRejected(context.Id,
                context.UserId, context.Name, code, message), context);
    }
}