using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class WorkerInfoMessage : QTMessage {

    [ProtoMember(2001)]
    public string ip;

    [ProtoMember(2002)]
    public string id;

    public WorkerInfoMessage() {
        messageType = type.WORKER_INFO;
    }
}
