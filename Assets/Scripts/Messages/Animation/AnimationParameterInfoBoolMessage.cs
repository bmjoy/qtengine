using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class AnimationParameterInfoBoolMessage : AnimationParameterInfoMessage {

    [ProtoMember(2001)]
    public bool value;

    public AnimationParameterInfoBoolMessage() : base() {
        syncedValueType = syncType.FLOAT;
    }
}
