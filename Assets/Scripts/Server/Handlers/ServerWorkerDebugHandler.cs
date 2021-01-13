using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerWorkerDebugHandler : BaseMessageHandler {

    public ServerWorkerDebugHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.WORKER_DEBUG:
                MasterServerQTClient qtRemoteClient = (MasterServerQTClient)client;
                WorkerDebugMessage debugMessage = (WorkerDebugMessage)message;

                QTDebugger.instance._debug(debugMessage.debugType, QTDebugger.debugType.NETWORK_WORKER, debugMessage.message);
                break;
        }
    }

}
