using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
public class RequestSyncMessage : QTMessage {

    public RequestSyncMessage() {
        messageType = type.REQUEST_SYNC;
    }
}
