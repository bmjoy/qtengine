using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class SessionMessage : QTMessage {

    [ProtoMember(2001)]
    public SessionInfo session;

    public SessionMessage() {
        messageType = type.SESSION;
    }
}
