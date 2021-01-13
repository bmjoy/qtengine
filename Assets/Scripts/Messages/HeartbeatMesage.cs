using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class HeartbeatMessage : QTMessage {

    [ProtoMember(2001)]
    public double serverTimestamp;

    [ProtoMember(2002)]
    public double createdTimestamp;

    public HeartbeatMessage() {
        messageType = type.HEARTBEAT;
    }
}
