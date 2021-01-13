using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class SyncMessage : QTMessage {

    [ProtoMember(2001)]
    public string objectID;
    [ProtoMember(2002)]
    public int stype;

    [ProtoMember(2003)]
    public float posX;
    [ProtoMember(2004)]
    public float posY;
    [ProtoMember(2005)]
    public float posZ;

    [ProtoMember(2006)]
    public float rotX;
    [ProtoMember(2007)]
    public float rotY;
    [ProtoMember(2008)]
    public float rotZ;

    [ProtoMember(2009)]
    public int index;

    public SyncMessage() {
        messageType = type.SYNC;
    }
}
