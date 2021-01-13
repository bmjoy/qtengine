using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class InputAxisMessage : QTMessage {

    [ProtoMember(2001)]
    public string axis;

    [ProtoMember(2002)]
    public float value;

    public InputAxisMessage() {
        messageType = type.INPUT_AXIS;
    }
}
