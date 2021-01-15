using System;
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

                component.clientComponent.setSyncedField(syncMessage.fieldName, QTUtils.getValueFromSyncFieldMessage(syncMessage));
                break;
            }

            case QTMessage.type.CALL_FUNCTION: {
                    try {
                        CallFunctionMessage callMessage = (CallFunctionMessage)message;
                        if (ClientManager.instance.spawnManager.spawnedObjects.ContainsKey(callMessage.objectID) == false) {
                            return;
                        }

                        ClientQTObject clientObject = (ClientQTObject)ClientManager.instance.spawnManager.spawnedObjects[callMessage.objectID];
                        BaseQTObjectComponent component = clientObject.objectComponents[callMessage.index];

                        List<object> parameters = new List<object>();
                        if (callMessage.parameters != null) {
                            foreach (SyncFieldMessage fieldMessage in callMessage.parameters) {
                                parameters.Add(QTUtils.getValueFromSyncFieldMessage(fieldMessage));
                            }
                        }

                        component.callFunction(callMessage.functionName, parameters.ToArray());
                    } catch(Exception e) {
                        Debug.LogError(e);
                    }
                break;
            }
        }
    }
}
