using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;
using Valve.VR;

[ProtoContract]
public class SyncVRPositionMessage : QTMessage {

    [ProtoMember(2001)]
    public SteamVR_Input_Sources source;

    [ProtoMember(2003)]
    public float posX;
    [ProtoMember(2004)]
    public float posY;
    [ProtoMember(2005)]
    public float posZ;

    public SyncVRPositionMessage() {
        messageType = type.SYNC_VR_POSITION;
    }
}
