using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class UserInfo {

    public enum userPermissions { USER, ADMIN };

    [ProtoMember(2001)]
    public string id;
    [ProtoMember(2002)]
    public string username;
    [ProtoMember(2003)]
    public string password;

    [ProtoMember(2004)]
    public userPermissions permissions;
}
