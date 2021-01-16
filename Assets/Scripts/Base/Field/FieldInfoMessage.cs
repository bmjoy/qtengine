using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
[ProtoInclude(1, typeof(FieldInfoIntMessage))]
[ProtoInclude(2, typeof(FieldInfoFloatMessage))]
[ProtoInclude(3, typeof(FieldInfoBoolMessage))]
[ProtoInclude(4, typeof(FieldInfoStringMessage))]
[ProtoInclude(5, typeof(FieldInfoArrayMessage))]
[ProtoInclude(6, typeof(FieldInfoDictionaryMessage))]
public class FieldInfoMessage : QTMessage {

    public enum syncType { INT, FLOAT, BOOL, STRING, ARRAY, DICTIONARY }

    [ProtoMember(2001)]
    public string objectID;
    [ProtoMember(2002)]
    public int index;

    [ProtoMember(2003)]
    public string fieldName;
    [ProtoMember(2004)]
    public syncType syncedValueType;

    public FieldInfoMessage() {
        messageType = type.SYNC_FIELD;
    }
}
