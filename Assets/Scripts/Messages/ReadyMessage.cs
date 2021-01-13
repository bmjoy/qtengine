using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class ReadyMessage : QTMessage {

    [ProtoMember(2001)]
    public bool state;

    public ReadyMessage() {
        messageType = type.READY;
    }
}
