using HotCode.System.Messaging.interfaces;
using Newtonsoft.Json;

namespace HotCode.Operation.Messages.Operations.Events
{
    public class OperationRejected : IEvent
    {
        public string Id { get; }
        public string UserId { get; }
        public string Name { get; }
        public string Code { get; }
        public string Message { get; }

        [JsonConstructor]
        public OperationRejected(string id, string userId, string name, string code, string message)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Code = code;
            Message = message;
        }
    }
}