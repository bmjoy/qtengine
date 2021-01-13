using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class RoomsMessage : QTMessage {

    [ProtoMember(2001)]
    public Dictionary<string, RoomInfo> rooms;

    public RoomsMessage() {
        messageType = type.ROOMS;
    }
}
