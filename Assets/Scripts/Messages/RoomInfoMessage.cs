using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class RoomInfoMessage : QTMessage {

    [ProtoMember(2001)]
    public RoomInfo room;

    public RoomInfoMessage() {
        messageType = type.ROOM_INFO;
    }
}
