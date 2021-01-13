using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInputEmitter : BaseBehaviour {

    public List<KeyCode> syncedKeys;
    public List<string> syncedAxis;

    public Dictionary<string, float> lastAxisValues;

    void Awake() {
        onStart += handleStart;
        onUpdate += handleUpdate;
    }

    public void handleStart() {
        if(syncedKeys == null) { syncedKeys = new List<KeyCode>(); }
        if(syncedAxis == null) { syncedAxis = new List<string>(); }

        lastAxisValues = new Dictionary<string, float>();
        foreach (string axis in syncedAxis) {
            lastAxisValues.Add(axis, Input.GetAxis(axis));
        }

        onKeyDown += emitKeyDown;
        onKeyUp += emitKeyUp;
    }

    public void handleUpdate() {
        foreach(string axis in lastAxisValues.Keys.ToList()) {
            float lastValue = lastAxisValues[axis];
            if(Input.GetAxis(axis) != lastValue) {
                emitAxisChange(axis, Input.GetAxis(axis));
            }

            lastAxisValues[axis] = Input.GetAxis(axis);
        }
    }

    void emitAxisChange(string axis, float value) {
        if (ClientManager.instance.state != BaseNetworking.componentState.RUNNING || ClientManager.instance.workerClient == null) { return; }

        InputAxisMessage message = new InputAxisMessage();
        message.axis = axis;
        message.value = value;
        ClientManager.instance.workerClient.sendMessage(message);
    }

    void emitKeyDown(KeyCode key) {
        if(ClientManager.instance.state != BaseNetworking.componentState.RUNNING || ClientManager.instance.workerClient == null || syncedKeys.Contains(key) == false) { return; }

        InputMessage message = new InputMessage();
        message.keyCode = key;
        message.active = true;
        ClientManager.instance.workerClient.sendMessage(message);
    }

    void emitKeyUp(KeyCode key) {
        if (ClientManager.instance.state != BaseNetworking.componentState.RUNNING || ClientManager.instance.workerClient == null || syncedKeys.Contains(key) == false) { return; }

        InputMessage message = new InputMessage();
        message.keyCode = key;
        message.active = false;
        ClientManager.instance.workerClient.sendMessage(message);
    }
}
