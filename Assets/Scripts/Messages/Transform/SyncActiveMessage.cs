using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class SyncActiveMessage : QTMessage {

    [ProtoMember(2001)]
    public string objectID;
    [ProtoMember(2002)]
    public int index;

    [ProtoMember(2003)]
    public bool value;

    public SyncActiveMessage() {
        messageType = type.SYNC_ACTIVE;
    }
}
