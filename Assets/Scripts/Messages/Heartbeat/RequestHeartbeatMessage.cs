using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class RequestHeartbeatMessage : QTResponsableMessage {

    [ProtoMember(2001)]
    public double createdTimestamp;

    public RequestHeartbeatMessage() : base() {
        messageType = type.REQUEST_HEARTBEAT;
    }
}
