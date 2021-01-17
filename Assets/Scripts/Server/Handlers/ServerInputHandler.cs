using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerInputHandler : BaseMessageHandler {

    public Action<KeyCode> onClientKeyDown;
    public Action<KeyCode> onClientKeyUp;

    public ServerInputHandler(BaseQTClient _client) : base(_client) {
        onClientKeyDown += onKeyDown;
        onClientKeyUp += onKeyUp;
    }

    public override void handleMessage(QTMessage message) {
        switch(message.messageType) {
            case QTMessage.type.INPUT:
                InputMessage inputMessage = (InputMessage)message;

                if(inputMessage.active) {
                    onClientKeyDown(inputMessage.keyCode);
                } else {
                    onClientKeyUp(inputMessage.keyCode);
                }
                break;

            case QTMessage.type.INPUT_AXIS:
                InputAxisMessage inputAxisMessage = (InputAxisMessage)message;
                updateAxisState(inputAxisMessage.axis, inputAxisMessage.value);
                break;

            case QTMessage.type.VR_ACTION:
                VRActionMessage actionMessage = (VRActionMessage)message;
                updateVRAction(actionMessage.actionName, QTUtils.getValueFromVRActionMessage(actionMessage));
                break;
        }
    }

    public void onKeyDown(KeyCode key) {
        updateState(key, true);
    }

    public void onKeyUp(KeyCode key) {
        updateState(key, false);
    }

    public void updateState(KeyCode key, bool state) {
        WorkerServerQTClient qtRemoteClient = (WorkerServerQTClient)client;

        if (qtRemoteClient.syncedKeys.ContainsKey(key) == false) {
            qtRemoteClient.syncedKeys.Add(key, state);
        } else {
            qtRemoteClient.syncedKeys[key] = state;
        }
    }

    public void updateAxisState(string axis, float value) {
        WorkerServerQTClient qtRemoteClient = (WorkerServerQTClient)client;

        if (qtRemoteClient.syncedAxis.ContainsKey(axis) == false) {
            qtRemoteClient.syncedAxis.Add(axis, value);
        } else {
            qtRemoteClient.syncedAxis[axis] = value;
        }
    }

    public void updateVRAction(string actionName, object value) {
        WorkerServerQTClient qtRemoteClient = (WorkerServerQTClient)client;

        if (qtRemoteClient.syncedVRActions.ContainsKey(actionName) == false) {
            qtRemoteClient.syncedVRActions.Add(actionName, value);
        } else {
            qtRemoteClient.syncedVRActions[actionName] = value;
        }
    }
}
