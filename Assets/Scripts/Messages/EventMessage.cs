using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class EventMessage : QTMessage {

    public enum evType { PING }

    [ProtoMember(2001)]
    public evType eventType;

    public EventMessage(evType _eventType) {
        eventType = _eventType;
        messageType = type.EVENT;
    }
}
