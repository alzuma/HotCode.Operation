using System.Threading.Tasks;
using HotCode.System.Messaging;

namespace HotCode.Operation.Services
{
    public interface IOperationPublisher
    {
        Task PendingAsync(CorrelationContext context);
        Task CompleteAsync(CorrelationContext context);
        Task RejectAsync(CorrelationContext context, string code, string message);
    }
}