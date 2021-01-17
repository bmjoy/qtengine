using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class VRActionBoolMessage : VRActionMessage {

    [ProtoMember(2001)]
    public bool value;

    public VRActionBoolMessage() : base() {
        syncedValueType = syncType.BOOL;
    }
}
