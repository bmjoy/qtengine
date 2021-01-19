using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class RequestRoomsMessage : QTResponsableMessage {

    public RequestRoomsMessage() : base() {
        messageType = type.REQUEST_ROOMS;
    }
}
