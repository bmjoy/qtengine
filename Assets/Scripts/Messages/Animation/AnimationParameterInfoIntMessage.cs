using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class AnimationParameterInfoIntMessage : AnimationParameterInfoMessage {

    [ProtoMember(2001)]
    public int value;

    public AnimationParameterInfoIntMessage() : base() {
        syncedValueType = syncType.INT;
    }
}
