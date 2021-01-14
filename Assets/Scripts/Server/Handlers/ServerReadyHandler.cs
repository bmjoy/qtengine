using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerReadyHandler : BaseMessageHandler {

    public ServerReadyHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.READY:
                WorkerServerQTClient qtRemoteClient = (WorkerServerQTClient)client;
                ReadyMessage readyMessage = (ReadyMessage)message;

                if (!qtRemoteClient.ready && readyMessage.state) {
                    qtRemoteClient.ready = readyMessage.state;

                    SessionMessage sessionMessage = new SessionMessage();
                    sessionMessage.session = qtRemoteClient.session;
                    qtRemoteClient.sendMessage(sessionMessage);

                    QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Client(" + client.getIP() + ") is ready...");
                    WorkerServerManager.instance.onClientReady(qtRemoteClient);
                } else {
                    qtRemoteClient.ready = readyMessage.state;
                }
                break;
        }
    }
}
