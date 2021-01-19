using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class WorkerDebugMessage : QTMessage {

    [ProtoMember(2001)]
    public string message;

    [ProtoMember(2002)]
    public QTDebugger.unityDebugType debugType;

    public WorkerDebugMessage() {
        messageType = type.WORKER_DEBUG;
    }
}
