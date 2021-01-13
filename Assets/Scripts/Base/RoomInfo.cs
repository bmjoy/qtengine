using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class RoomInfo {

    [ProtoMember(2001)]
    public string id;

    [ProtoMember(2002)]
    public int port;

    [ProtoMember(2003)]
    public string ip = "127.0.0.1";

}
