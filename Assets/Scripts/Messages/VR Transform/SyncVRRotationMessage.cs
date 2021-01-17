using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;
using Valve.VR;

[ProtoContract]
public class SyncVRRotationMessage : QTMessage {

    [ProtoMember(2001)]
    public SteamVR_Input_Sources source;

    [ProtoMember(2003)]
    public float rotX;
    [ProtoMember(2004)]
    public float rotY;
    [ProtoMember(2005)]
    public float rotZ;

    public SyncVRRotationMessage() {
        messageType = type.SYNC_VR_ROTATION;
    }
}
