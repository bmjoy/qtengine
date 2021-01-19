using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
[ProtoInclude(1, typeof(RequestRoomsMessage))]
[ProtoInclude(2, typeof(RoomsMessage))]
[ProtoInclude(3, typeof(LoginMessage))]
[ProtoInclude(4, typeof(LoginReplyMessage))]
[ProtoInclude(5, typeof(RegisterMessage))]
[ProtoInclude(6, typeof(RegisterReplyMessage))]
//[ProtoInclude(7, typeof(RequestNewInstanceMessage))]
//[ProtoInclude(8, typeof(InstanceMessage))]
[ProtoInclude(9, typeof(RequestHeartbeatMessage))]
[ProtoInclude(10, typeof(HeartbeatMessage))]
public class QTResponsableMessage : QTMessage {

    [ProtoMember(2001)]
    public string responseID = Guid.NewGuid().ToString();

    public Action<QTResponsableMessage> onResponse;

    public QTResponsableMessage(QTResponsableMessage sourceMessage=null) {
        onResponse += debugResponse;

        if(sourceMessage != null) {
            responseID = sourceMessage.responseID;
        } else {
            responseID = Guid.NewGuid().ToString();
        }
    }

    public void debugResponse(QTResponsableMessage message) {
        QTDebugger.instance.debug(QTDebugger.debugType.NETWORK, "Got response for message with ID - " + message.responseID);
    }
}
