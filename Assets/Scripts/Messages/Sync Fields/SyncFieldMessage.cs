using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
[ProtoInclude(1, typeof(SyncIntMessage))]
[ProtoInclude(2, typeof(SyncFloatMessage))]
[ProtoInclude(3, typeof(SyncBoolMessage))]
[ProtoInclude(4, typeof(SyncStringMessage))]
public class SyncFieldMessage : QTMessage {

    public enum syncType { INT, FLOAT, BOOL, STRING }

    [ProtoMember(2001)]
    public string objectID;
    [ProtoMember(2002)]
    public int index;

    [ProtoMember(2003)]
    public string fieldName;
    [ProtoMember(2004)]
    public syncType syncedValueType;

    public SyncFieldMessage() {
        messageType = type.SYNC_FIELD;
    }
}
