using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class FieldInfoFloatMessage : FieldInfoMessage {

    [ProtoMember(2001)]
    public float value;

    public FieldInfoFloatMessage() : base() {
        syncedValueType = syncType.FLOAT;
    }
}
