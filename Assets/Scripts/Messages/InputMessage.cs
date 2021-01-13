using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class InputMessage : QTMessage {

    [ProtoMember(2001)]
    public bool active;

    [ProtoMember(2002)]
    public KeyCode keyCode;

    public InputMessage() {
        messageType = type.INPUT;
    }
}
