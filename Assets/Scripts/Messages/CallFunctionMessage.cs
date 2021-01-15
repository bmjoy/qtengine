using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class CallFunctionMessage : QTMessage {

    [ProtoMember(2001)]
    public string objectID;
    [ProtoMember(2002)]
    public int index;

    [ProtoMember(2003)]
    public string functionName;

    public CallFunctionMessage() {
        messageType = type.CALL_FUNCTION;
    }
}
