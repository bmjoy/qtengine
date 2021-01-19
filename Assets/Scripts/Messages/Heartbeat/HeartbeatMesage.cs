using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class HeartbeatMessage : QTResponsableMessage {

    [ProtoMember(2001)]
    public double serverTimestamp;

    [ProtoMember(2002)]
    public double createdTimestamp;

    public HeartbeatMessage(QTResponsableMessage _message=null) : base(_message) {
        messageType = type.HEARTBEAT;
    }
}
