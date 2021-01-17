using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class VRActionVector2Message : VRActionMessage {

    [ProtoMember(2001)]
    public float value_0;
    [ProtoMember(2002)]
    public float value_1;

    public VRActionVector2Message() : base() {
        syncedValueType = syncType.VECTOR2;
    }
}
