using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerWorkerInfoHandler : BaseMessageHandler {

    public ServerWorkerInfoHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.WORKER_INFO:
                MasterServerQTClient qtRemoteClient = (MasterServerQTClient)client;
                WorkerInfoMessage workerInfoMessage = (WorkerInfoMessage)message;

                qtRemoteClient.remoteType = QTClient.clientType.WORKER_SERVER;
                //MasterServerManager.instance.workersManager.workers[workerInfoMessage.id].room.ip = workerInfoMessage.ip;
                break;
        }
    }

}
