using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class VRActionVector1Message : VRActionMessage {

    [ProtoMember(2001)]
    public float value;

    public VRActionVector1Message() : base() {
        syncedValueType = syncType.VECTOR1;
    }
}
