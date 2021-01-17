using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ClientVRInputEmitter : BaseBehaviour {

    public List<SteamVR_Action_Boolean> syncedVRBooleanActions;
    public List<SteamVR_Action_Single> syncedVRVector1Actions;
    public List<SteamVR_Action_Vector2> syncedVRVector2Actions;

    public Dictionary<string, object> lastVRActionValues;

    void Awake() {
        onStart += handleStart;
        onUpdate += handleUpdate;
    }

    public void handleStart() {
        if (syncedVRBooleanActions == null) { syncedVRBooleanActions = new List<SteamVR_Action_Boolean>(); }
        if (syncedVRVector1Actions == null) { syncedVRVector1Actions = new List<SteamVR_Action_Single>(); }
        if (syncedVRVector2Actions == null) { syncedVRVector2Actions = new List<SteamVR_Action_Vector2>(); }

        lastVRActionValues = new Dictionary<string, object>();
        foreach (SteamVR_Action_Boolean action in syncedVRBooleanActions) {
            lastVRActionValues.Add(action.GetShortName(), action.state);
        }
        foreach (SteamVR_Action_Single action in syncedVRVector1Actions) {
            lastVRActionValues.Add(action.GetShortName(), action.axis);
        }
        foreach (SteamVR_Action_Vector2 action in syncedVRVector2Actions) {
            lastVRActionValues.Add(action.GetShortName(), action.axis);
        }
    }

    public void handleUpdate() {
        foreach (SteamVR_Action_Boolean action in syncedVRBooleanActions) {
            object lastValue = lastVRActionValues[action.GetShortName()];
            if (action.state != (bool)lastValue) {
                emitVRActionBooleanChange(action, action.state);
            }

            lastVRActionValues[action.GetShortName()] = action.state;
        }

        foreach (SteamVR_Action_Single action in syncedVRVector1Actions) {
            object lastValue = lastVRActionValues[action.GetShortName()];
            if (action.axis != (float)lastValue) {
                emitVRActionVector1Change(action, action.axis);
            }

            lastVRActionValues[action.GetShortName()] = action.axis;
        }

        foreach (SteamVR_Action_Vector2 action in syncedVRVector2Actions) {
            object lastValue = lastVRActionValues[action.GetShortName()];
            if (action.axis != (Vector2)lastValue) {
                emitVRActionVector2Change(action, action.axis);
            }

            lastVRActionValues[action.GetShortName()] = action.axis;
        }
    }

    void emitVRActionBooleanChange(SteamVR_Action_Boolean action, bool value) {
        if (ClientManager.instance.state != BaseNetworking.componentState.RUNNING || ClientManager.instance.workerClient == null) { return; }
        Debug.Log("sending boolean of " + action.GetShortName() + "=" + value);

        VRActionBoolMessage message = new VRActionBoolMessage();
        message.actionName = action.GetShortName();
        message.value = value;
        ClientManager.instance.workerClient.sendMessage(message);
    }

    void emitVRActionVector1Change(SteamVR_Action_Single action, float value) {
        if (ClientManager.instance.state != BaseNetworking.componentState.RUNNING || ClientManager.instance.workerClient == null) { return; }
        Debug.Log("sending vector1 of " + action.GetShortName() + "=" + value);

        VRActionVector1Message message = new VRActionVector1Message();
        message.actionName = action.GetShortName();
        message.value = value;
        ClientManager.instance.workerClient.sendMessage(message);
    }

    void emitVRActionVector2Change(SteamVR_Action_Vector2 action, Vector2 value) {
        if (ClientManager.instance.state != BaseNetworking.componentState.RUNNING || ClientManager.instance.workerClient == null) { return; }
        Debug.Log("sending vector2 of " + action.GetShortName() + "=" + value.x + "-" + value.y);

        VRActionVector2Message message = new VRActionVector2Message();
        message.actionName = action.GetShortName();
        message.value_0 = value.x;
        message.value_1 = value.y;
        ClientManager.instance.workerClient.sendMessage(message);
    }
}
