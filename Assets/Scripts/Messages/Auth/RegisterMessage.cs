using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class RegisterMessage : QTResponsableMessage {

    [ProtoMember(2001)]
    public string username;

    [ProtoMember(2002)]
    public string password;

    public RegisterMessage() : base() {
        messageType = type.REGISTER;
    }
}
