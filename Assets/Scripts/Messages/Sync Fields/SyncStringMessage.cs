using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class SyncStringMessage : SyncFieldMessage {

    [ProtoMember(2001)]
    public string value;

    public SyncStringMessage() : base() {
        syncedValueType = syncType.STRING;
    }
}
