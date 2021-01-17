using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ClientVRInputEmitter : BaseBehaviour {

    public SteamVR_Action_Pose poseAction = SteamVR_Input.GetAction<SteamVR_Action_Pose>("Pose");

    public List<SteamVR_Input_Sources> syncedVRSources;
    public List<SteamVR_Action_Boolean> syncedVRBooleanActions;
    public List<SteamVR_Action_Single> syncedVRVector1Actions;
    public List<SteamVR_Action_Vector2> syncedVRVector2Actions;

    public Dictionary<string, object> lastVRActionValues;
    public Dictionary<SteamVR_Input_Sources, Vector3> lastVRPositions;
    public Dictionary<SteamVR_Input_Sources, Vector3> lastVRRotations;

    void Awake() {
        onStart += handleStart;
        onUpdate += handleUpdate;

        onKeyDown += debug;
    }

    public void debug(KeyCode key) {
        if(key == KeyCode.R) {
            Debug.Log("lhand: " + poseAction[SteamVR_Input_Sources.LeftHand].deviceIsConnected);
            Debug.Log("rhand: " + poseAction[SteamVR_Input_Sources.RightHand].deviceIsConnected);
            Debug.Log("lfoot: " + poseAction[SteamVR_Input_Sources.LeftFoot].deviceIsConnected);
            Debug.Log("rfoot: " + poseAction[SteamVR_Input_Sources.RightFoot].deviceIsConnected);
            Debug.Log("head: " + poseAction[SteamVR_Input_Sources.Head].deviceIsConnected);
            Debug.Log("waist: " + poseAction[SteamVR_Input_Sources.Waist].deviceIsConnected);
        }
    }

    public void handleStart() {
        if (syncedVRSources == null) { syncedVRSources = new List<SteamVR_Input_Sources>(); }
        if (syncedVRBooleanActions == null) { syncedVRBooleanActions = new List<SteamVR_Action_Boolean>(); }
        if (syncedVRVector1Actions == null) { syncedVRVector1Actions = new List<SteamVR_Action_Single>(); }
        if (syncedVRVector2Actions == null) { syncedVRVector2Actions = new List<SteamVR_Action_Vector2>(); }

        lastVRActionValues = new Dictionary<string, object>();
        lastVRPositions = new Dictionary<SteamVR_Input_Sources, Vector3>();
        lastVRRotations = new Dictionary<SteamVR_Input_Sources, Vector3>();
        foreach (SteamVR_Action_Boolean action in syncedVRBooleanActions) {
            lastVRActionValues.Add(action.GetShortName(), action.state);
        }
        foreach (SteamVR_Action_Single action in syncedVRVector1Actions) {
            lastVRActionValues.Add(action.GetShortName(), action.axis);
        }
        foreach (SteamVR_Action_Vector2 action in syncedVRVector2Actions) {
            lastVRActionValues.Add(action.GetShortName(), action.axis);
        }

        foreach (SteamVR_Input_Sources source in syncedVRSources) {
            lastVRPositions.Add(source, poseAction[source].localPosition);
            lastVRRotations.Add(source, poseAction[source].localRotation.eulerAngles);
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

        foreach (SteamVR_Input_Sources source in syncedVRSources) {
            Vector3 lastValue = lastVRPositions[source];
            if (poseAction[source].localPosition != lastValue) {
                emitVRPositionChange(source, poseAction[source].localPosition);
            }

            lastVRPositions[source] = poseAction[source].localPosition;
        }

        foreach (SteamVR_Input_Sources source in syncedVRSources) {
            Vector3 lastValue = lastVRRotations[source];
            if (poseAction[source].localRotation.eulerAngles != lastValue) {
                emitVRRotationChange(source, poseAction[source].localRotation.eulerAngles);
            }

            lastVRRotations[source] = poseAction[source].localRotation.eulerAngles;
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

    void emitVRPositionChange(SteamVR_Input_Sources source, Vector3 value) {
        if (ClientManager.instance.state != BaseNetworking.componentState.RUNNING || ClientManager.instance.workerClient == null) { return; }
        Debug.Log("sending position of " + source.ToString() + "=" + value.x + "-" + value.y + "-" + value.z);

        SyncVRPositionMessage message = new SyncVRPositionMessage();
        message.source = source;
        message.posX = value.x;
        message.posY = value.y;
        message.posZ = value.z;
        ClientManager.instance.workerClient.sendMessage(message);
    }

    void emitVRRotationChange(SteamVR_Input_Sources source, Vector3 value) {
        if (ClientManager.instance.state != BaseNetworking.componentState.RUNNING || ClientManager.instance.workerClient == null) { return; }
        Debug.Log("sending rotation of " + source.ToString() + "=" + value.x + "-" + value.y + "-" + value.z);

        SyncVRRotationMessage message = new SyncVRRotationMessage();
        message.source = source;
        message.rotX = value.x;
        message.rotY = value.y;
        message.rotZ = value.z;
        ClientManager.instance.workerClient.sendMessage(message);
    }
}
