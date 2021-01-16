using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSyncHandler : BaseMessageHandler {

    public ClientSyncHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch(message.messageType) {
            case QTMessage.type.SYNC_POSITION: {
                SyncPositionMessage syncMessage = (SyncPositionMessage)message;
                if(ClientManager.instance.spawnManager.spawnedObjects.ContainsKey(syncMessage.objectID) == false) {
                    return;
                }

                ClientQTObject clientObject = (ClientQTObject)ClientManager.instance.spawnManager.spawnedObjects[syncMessage.objectID];
                SyncGameobject syncComponent = (SyncGameobject)clientObject.objectComponents[syncMessage.index];
                syncComponent.syncedPosition = new Vector3(syncMessage.posX, syncMessage.posY, syncMessage.posZ);
                break;
            }

            case QTMessage.type.SYNC_ROTATION: {
                SyncRotationMessage syncMessage = (SyncRotationMessage)message;
                if(ClientManager.instance.spawnManager.spawnedObjects.ContainsKey(syncMessage.objectID) == false) {
                    return;
                }

                ClientQTObject clientObject = (ClientQTObject)ClientManager.instance.spawnManager.spawnedObjects[syncMessage.objectID];
                SyncGameobject syncComponent = (SyncGameobject)clientObject.objectComponents[syncMessage.index];
                syncComponent.syncedRotation = new Vector3(syncMessage.rotX, syncMessage.rotY, syncMessage.rotZ);
                break;
            }

            case QTMessage.type.SYNC_FIELD: {
                FieldInfoMessage syncMessage = (FieldInfoMessage)message;
                if (ClientManager.instance.spawnManager.spawnedObjects.ContainsKey(syncMessage.objectID) == false) {
                    return;
                }

                ClientQTObject clientObject = (ClientQTObject)ClientManager.instance.spawnManager.spawnedObjects[syncMessage.objectID];
                BaseQTObjectComponent component = clientObject.objectComponents[syncMessage.index];

                component.clientComponent.setSyncedField(syncMessage.fieldName, QTUtils.getValueFromSyncFieldMessage(syncMessage));
                break;
            }

            case QTMessage.type.SYNC_ANIMATION: {
                AnimationParameterInfoMessage syncMessage = (AnimationParameterInfoMessage)message;
                if (ClientManager.instance.spawnManager.spawnedObjects.ContainsKey(syncMessage.objectID) == false) {
                    return;
                }

                ClientQTObject clientObject = (ClientQTObject)ClientManager.instance.spawnManager.spawnedObjects[syncMessage.objectID];
                SyncAnimation component = (SyncAnimation)clientObject.objectComponents[syncMessage.index];

                QTUtils.applySyncAnimationMessageToAnimator(component.animator, syncMessage);
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
                            foreach (FieldInfoMessage fieldMessage in callMessage.parameters) {
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
