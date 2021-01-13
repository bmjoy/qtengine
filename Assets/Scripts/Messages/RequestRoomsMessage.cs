using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class RequestRoomsMessage : QTMessage {

    public RequestRoomsMessage() {
        messageType = type.REQUEST_ROOMS;
    }
}
