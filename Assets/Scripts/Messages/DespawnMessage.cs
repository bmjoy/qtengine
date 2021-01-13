using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;
using UnityEngine;

[ProtoContract]
public class DespawnMessage : QTMessage {

    [ProtoMember(2001)]
    public string objectID;

    public DespawnMessage() {
        messageType = type.DESPAWN;
    }
}
