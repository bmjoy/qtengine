using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientOwnerHandler : BaseMessageHandler {

    public ClientOwnerHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.OWNER:
                QTClient qtClient = (QTClient)client;
                OwnerMessage ownerMessage = (OwnerMessage)message;

                ClientManager.instance.spawnManager.spawnedObjects[ownerMessage.objectID].setOwner(ownerMessage.ownerID);
                break;
        }
    }
}
