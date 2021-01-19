using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class WorkerReadyMessage : QTMessage {

    [ProtoMember(2001)]
    public string id;

    public WorkerReadyMessage() {
        messageType = type.WORKER_READY;
    }
}
