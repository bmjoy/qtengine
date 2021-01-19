using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class LoginReplyMessage : QTResponsableMessage {

    public enum loginReply { SUCCESS, WRONGPASSWORD, NOUSER }

    [ProtoMember(2001)]
    public loginReply reply;

    [ProtoMember(2002)]
    public UserInfo user;

    public LoginReplyMessage(QTResponsableMessage _message = null) : base(_message) {
        messageType = type.LOGIN_REPLY;
    }

}
