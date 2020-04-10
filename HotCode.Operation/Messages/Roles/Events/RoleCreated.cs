﻿using HotCode.System.Messaging;
 using HotCode.System.Messaging.interfaces;

 namespace HotCode.Operation.Messages.Roles.Events
{
    [MessageNamespace("HotCode.StrongHold")]
    public class RoleCreated : IEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}