using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class SessionInfo {

    [ProtoMember(2001)]
    public string id;

    [ProtoMember(2002)]
    public UserInfo user;

}
