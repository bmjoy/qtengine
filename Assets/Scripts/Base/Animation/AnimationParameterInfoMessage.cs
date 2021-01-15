using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
[ProtoInclude(1, typeof(AnimationParameterInfoIntMessage))]
[ProtoInclude(2, typeof(AnimationParameterInfoFloatMessage))]
[ProtoInclude(3, typeof(AnimationParameterInfoBoolMessage))]
public class AnimationParameterInfoMessage : QTMessage {

    public enum syncType { INT, FLOAT, BOOL }

    [ProtoMember(2001)]
    public string objectID;
    [ProtoMember(2002)]
    public int index;

    [ProtoMember(2003)]
    public string fieldName;
    [ProtoMember(2004)]
    public syncType syncedValueType;

    public AnimationParameterInfoMessage() {
        messageType = type.SYNC_ANIMATION;
    }

}
