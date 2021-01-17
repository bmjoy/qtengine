using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class FieldInfoDictionaryMessage : FieldInfoMessage {

    [ProtoMember(2001)]
    public Dictionary<FieldInfoMessage, FieldInfoMessage> value;

    public FieldInfoDictionaryMessage() : base() {
        syncedValueType = syncType.DICTIONARY;
    }
}
