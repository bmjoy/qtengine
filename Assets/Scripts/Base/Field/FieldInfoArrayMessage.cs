using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class FieldInfoArrayMessage : FieldInfoMessage {

    [ProtoMember(2001)]
    public FieldInfoMessage[] value;

    public FieldInfoArrayMessage() : base() {
        syncedValueType = syncType.ARRAY;
    }
}
