using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class FieldInfoStringMessage : FieldInfoMessage {

    [ProtoMember(2001)]
    public string value;

    public FieldInfoStringMessage() : base() {
        syncedValueType = syncType.STRING;
    }
}
