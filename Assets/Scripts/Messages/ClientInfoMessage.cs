using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class ClientInfoMessage : QTMessage {

    public ClientInfoMessage() {
        messageType = type.CLIENT_INFO;
    }
}
