using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class RequestHeartbeatMessage : QTMessage {

    [ProtoMember(2001)]
    public double createdTimestamp;

    public RequestHeartbeatMessage() {
        messageType = type.REQUEST_HEARTBEAT;
    }
}
