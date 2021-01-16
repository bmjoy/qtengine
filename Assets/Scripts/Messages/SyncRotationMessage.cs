using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class SyncRotationMessage : QTMessage {

    [ProtoMember(2001)]
    public string objectID;
    [ProtoMember(2002)]
    public int index;

    [ProtoMember(2003)]
    public float rotX;
    [ProtoMember(2004)]
    public float rotY;
    [ProtoMember(2005)]
    public float rotZ;

    public SyncRotationMessage() {
        messageType = type.SYNC_ROTATION;
    }
}
