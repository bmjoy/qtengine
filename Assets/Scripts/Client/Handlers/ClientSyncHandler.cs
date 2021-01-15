using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSyncHandler : BaseMessageHandler {

    public ClientSyncHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch(message.messageType) {
            case QTMessage.type.SYNC: {
                SyncMessage syncMessage = (SyncMessage)message;
                if(ClientManager.instance.spawnManager.spawnedObjects.ContainsKey(syncMessage.objectID) == false) {
                    return;
                }

                ClientQTObject clientObject = (ClientQTObject)ClientManager.instance.spawnManager.spawnedObjects[syncMessage.objectID];
                SyncGameobject syncComponent = (SyncGameobject)clientObject.objectComponents[syncMessage.index];

                switch (syncMessage.stype) {
                    case 0:
                        syncComponent.syncedPosition = new Vector3(syncMessage.posX, syncMessage.posY, syncMessage.posZ);
                        break;

                    case 1:
                        syncComponent.syncedRotation = new Vector3(syncMessage.rotX, syncMessage.rotY, syncMessage.rotZ);
                        break;
                }

                break;
            }

            case QTMessage.type.SYNC_FIELD: {
                SyncFieldMessage syncMessage = (SyncFieldMessage)message;
                if (ClientManager.instance.spawnManager.spawnedObjects.ContainsKey(syncMessage.objectID) == false) {
                    return;
                }

                ClientQTObject clientObject = (ClientQTObject)ClientManager.instance.spawnManager.spawnedObjects[syncMessage.objectID];
                BaseQTObjectComponent component = clientObject.objectComponents[syncMessage.index];

                switch (syncMessage.syncedValueType) {
                    case SyncFieldMessage.syncType.INT: {
                        SyncIntMessage syncMessageDetailed = (SyncIntMessage)message;
                        component.clientComponent.setSyncedField(syncMessageDetailed.fieldName, syncMessageDetailed.value);
                        break;
                    }

                    case SyncFieldMessage.syncType.FLOAT: {
                        SyncFloatMessage syncMessageDetailed = (SyncFloatMessage)message;
                        component.clientComponent.setSyncedField(syncMessageDetailed.fieldName, syncMessageDetailed.value);
                        break;
                    }

                    case SyncFieldMessage.syncType.BOOL: {
                        SyncBoolMessage syncMessageDetailed = (SyncBoolMessage)message;
                        component.clientComponent.setSyncedField(syncMessageDetailed.fieldName, syncMessageDetailed.value);
                        break;
                    }

                    case SyncFieldMessage.syncType.STRING: {
                        SyncStringMessage syncMessageDetailed = (SyncStringMessage)message;
                        component.clientComponent.setSyncedField(syncMessageDetailed.fieldName, syncMessageDetailed.value);
                        break;
                    }
                }

                break;
            }

        }
    }
}
