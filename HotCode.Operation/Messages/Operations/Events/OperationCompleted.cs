using HotCode.System.Messaging.interfaces;
using Newtonsoft.Json;

namespace HotCode.Operation.Messages.Operations.Events
{
    public class OperationCompleted : IEvent
    {
        public string Id { get; }
        public string UserId { get; }
        public string Name { get; }

        [JsonConstructor]
        public OperationCompleted(string id,
            string userId, string name)
        {
            Id = id;
            UserId = userId;
            Name = name;
        }
    }
}