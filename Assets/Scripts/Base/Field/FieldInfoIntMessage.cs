using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class FieldInfoIntMessage : FieldInfoMessage {

    [ProtoMember(2001)]
    public int value;

    public FieldInfoIntMessage() : base() {
        syncedValueType = syncType.INT;
    }
}
