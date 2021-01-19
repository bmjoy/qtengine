using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawnHandler : BaseMessageHandler {

    public ClientSpawnHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.SPAWN:
                SpawnMessage spawnMessage = (SpawnMessage)message;
                BaseQTObject obj = ClientManager.instance.spawnManager.spawnObject(spawnMessage.objectID, spawnMessage.prefabName, spawnMessage.spawnPosition, spawnMessage.spawnRotation);
                obj.gameObject.SetActive(spawnMessage.active);
                break;

            case QTMessage.type.DESPAWN:
                DespawnMessage despawnMessage = (DespawnMessage)message;
                ClientManager.instance.spawnManager.despawnObject(despawnMessage.objectID);
                break;
        }
    }

}
