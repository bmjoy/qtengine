using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerRequestSyncHandler : BaseMessageHandler {

    public ServerRequestSyncHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.REQUEST_SYNC:
                WorkerServerQTClient qtRemoteClient = (WorkerServerQTClient)client;

                foreach(ServerQTObject obj in WorkerServerManager.instance.spawnManager.spawnedObjects.Values) {
                    SpawnMessage spawnMessage = new SpawnMessage();
                    spawnMessage.objectID = obj.objectID;
                    spawnMessage.prefabName = obj.prefabName;
                    spawnMessage.spawnPosition = QTUtils.getSyncMessageFromObject(obj);
                    qtRemoteClient.sendMessage(spawnMessage);

                    foreach(BaseQTObjectComponent comp in obj.objectComponents.Values) {
                        comp.serverComponent.syncAllFields();
                    }
                }
                break;
        }
    }

}
