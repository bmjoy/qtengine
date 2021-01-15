using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class AnimationParameterInfoFloatMessage : AnimationParameterInfoMessage {

    [ProtoMember(2001)]
    public float value;

    public AnimationParameterInfoFloatMessage() : base() {
        syncedValueType = syncType.FLOAT;
    }
}
