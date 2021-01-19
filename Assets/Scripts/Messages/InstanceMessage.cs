using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class InstanceMessage : QTResponsableMessage {

    [ProtoMember(2001)]
    public RoomInfo room;

    public InstanceMessage(QTResponsableMessage _message = null) : base(_message) {
        messageType = type.INSTANCE;
    }

}
