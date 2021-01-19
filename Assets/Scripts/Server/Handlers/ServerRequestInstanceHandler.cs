using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerRequestInstanceHandler : BaseMessageHandler {

    public ServerRequestInstanceHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.REQUEST_NEW_INSTANCE:
                MasterServerQTClient qtRemoteClient = (MasterServerQTClient)client;
                RequestNewInstanceMessage requestMessage = (RequestNewInstanceMessage)message;

                MasterServerWorker worker = MasterServerManager.instance.workersManager.spawnWorker(Guid.NewGuid().ToString());

                InstanceMessage instanceMessage = new InstanceMessage(requestMessage);
                instanceMessage.room = worker.room;
                worker.onWorkerReady += () => {
                    sendRequestInstanceResponse(qtRemoteClient, instanceMessage);
                };
                break;
        }
    }

    public void sendRequestInstanceResponse(MasterServerQTClient qtRemoteClient, InstanceMessage message) {
        qtRemoteClient.sendMessage(message);
    }

}
