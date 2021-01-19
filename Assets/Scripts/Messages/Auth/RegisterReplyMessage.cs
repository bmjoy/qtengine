using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class RegisterReplyMessage : QTResponsableMessage {

    public enum registerReply { SUCCESS, USEREXISTS }

    [ProtoMember(2001)]
    public registerReply reply;

    [ProtoMember(2002)]
    public UserInfo user;

    public RegisterReplyMessage(QTResponsableMessage _message = null) : base(_message) {
        messageType = type.REGISTER_REPLY;
    }

}
