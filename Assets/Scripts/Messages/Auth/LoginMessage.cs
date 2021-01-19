using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class LoginMessage : QTResponsableMessage {

    [ProtoMember(2001)]
    public string username;

    [ProtoMember(2002)]
    public string password;

    public LoginMessage() : base() {
        messageType = type.LOGIN;
    }
}
