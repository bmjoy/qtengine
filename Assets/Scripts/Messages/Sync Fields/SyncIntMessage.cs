using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class SyncIntMessage : SyncFieldMessage {

    [ProtoMember(2001)]
    public int value;

    public SyncIntMessage() : base() {
        syncedValueType = syncType.INT;
    }
}
