using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;
using UnityEngine;

[ProtoContract]
public class SpawnMessage : QTMessage {

    [ProtoMember(2001)]
    public string objectID;
    [ProtoMember(2002)]
    public string prefabName;

    [ProtoMember(2003)]
    public SyncPositionMessage spawnPosition;
    [ProtoMember(2004)]
    public SyncRotationMessage spawnRotation;
    [ProtoMember(2005)]
    public bool active;

    public SpawnMessage() {
        messageType = type.SPAWN;
    }
}
