using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class SyncFloatMessage : SyncFieldMessage {

    [ProtoMember(2001)]
    public float value;

    public SyncFloatMessage() : base() {
        syncedValueType = syncType.FLOAT;
    }
}
