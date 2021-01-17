using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class FieldInfoBoolMessage : FieldInfoMessage {

    [ProtoMember(2001)]
    public bool value;

    public FieldInfoBoolMessage() : base() {
        syncedValueType = syncType.BOOL;
    }
}
