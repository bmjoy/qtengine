using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class SyncBoolMessage : SyncFieldMessage {

    [ProtoMember(2001)]
    public bool value;

    public SyncBoolMessage() : base() {
        syncedValueType = syncType.BOOL;
    }
}
