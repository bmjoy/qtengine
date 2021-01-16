using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class SyncPositionMessage : QTMessage {

    [ProtoMember(2001)]
    public string objectID;
    [ProtoMember(2002)]
    public int index;

    [ProtoMember(2003)]
    public float posX;
    [ProtoMember(2004)]
    public float posY;
    [ProtoMember(2005)]
    public float posZ;

    public SyncPositionMessage() {
        messageType = type.SYNC_POSITION;
    }
}
