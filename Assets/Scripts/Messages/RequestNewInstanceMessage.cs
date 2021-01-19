using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class RequestNewInstanceMessage : QTResponsableMessage {

    public RequestNewInstanceMessage() : base() {
        messageType = type.REQUEST_NEW_INSTANCE;
    }

}
