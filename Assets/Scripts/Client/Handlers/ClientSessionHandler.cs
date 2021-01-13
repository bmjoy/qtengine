using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSessionHandler : BaseMessageHandler {

    public ClientSessionHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.SESSION:
                QTClient qtClient = (QTClient)client;
                SessionMessage sessionMessage = (SessionMessage)message;

                qtClient.session = sessionMessage.session;
                QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Recieved new session(" + sessionMessage.session.id + ")-");
                break;
        }
    }
}
