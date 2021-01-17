using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
[ProtoInclude(1, typeof(VRActionBoolMessage))]
[ProtoInclude(2, typeof(VRActionVector1Message))]
[ProtoInclude(3, typeof(VRActionVector2Message))]
public class VRActionMessage : QTMessage {

    public enum syncType { BOOL, VECTOR1, VECTOR2 }

    [ProtoMember(2001)]
    public string actionName;
    [ProtoMember(2004)]
    public syncType syncedValueType;

    public VRActionMessage() {
        messageType = type.VR_ACTION;
    }
}
