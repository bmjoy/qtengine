using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class RoomsMessage : QTResponsableMessage {

    [ProtoMember(2001)]
    public Dictionary<string, RoomInfo> rooms;

    public RoomsMessage(QTResponsableMessage _message = null) : base(_message) {
        messageType = type.ROOMS;
    }
}
