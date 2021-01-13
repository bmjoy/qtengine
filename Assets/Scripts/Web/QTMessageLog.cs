using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class QTMessageLog {

    public enum messageDirection { SERVERTOCLIENT, CLIENTTOSERVER };

    public int index;
    public QTMessage.type type;
    public int messageSize;
    public string identity;
    public messageDirection direction;

    public QTMessageLog(int _index, QTMessage _message, BaseQTClient _client) {
        index = _index;
        type = _message.messageType;
        messageSize = QTUtils.ObjectToByteArray(_message).Length;
        identity = _client.getIP();
    }
}
