using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerWorkerInfoHandler : BaseMessageHandler {

    public ServerWorkerInfoHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.WORKER_READY: {
                MasterServerQTClient qtRemoteClient = (MasterServerQTClient)client;
                WorkerReadyMessage workerReadyMessage = (WorkerReadyMessage)message;

                qtRemoteClient.remoteType = QTClient.clientType.WORKER_SERVER;
                MasterServerWorker worker = MasterServerManager.instance.workersManager.workers[workerReadyMessage.id];

                RoomInfoMessage roomInfoMessage = new RoomInfoMessage();
                roomInfoMessage.room = worker.room;
                qtRemoteClient.sendMessage(roomInfoMessage);

                QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Worker(" + worker.room.id + ") is ready...");
                break;
            }

            case QTMessage.type.WORKER_INFO: {
                MasterServerQTClient qtRemoteClient = (MasterServerQTClient)client;
                WorkerInfoMessage workerInfoMessage = (WorkerInfoMessage)message;
                MasterServerWorker worker = MasterServerManager.instance.workersManager.workers[workerInfoMessage.id];

                worker.room.ip = workerInfoMessage.ip;

                QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Recieved info from Worker(" + worker.room.id + ")...");
                break;
            }
        }
    }

}
