using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncGameobject : BaseQTObjectComponent {

    /* Client/Server */
    [HideInInspector]
    public int index;

    /* Server */
    public bool syncPosition, syncRotation;
    public bool useInterpolation = false;

    [HideInInspector]
    public Vector3 lastPosition;
    [HideInInspector]
    public Vector3 lastRotation;

    /* Client */
    [HideInInspector]
    public Vector3 syncedPosition;
    [HideInInspector]
    public Vector3 syncedRotation;

    public override void handleObjectSpawn() {
        handleClientObjectSpawn();
        handleServerObjectSpawn();
    }

    #region Server Part
    public override void handleServerObjectSpawn() {
        if (obj.objectType != BaseQTObject.type.SERVER) { return; }
        InvokeRepeating("sync", 0f, ServerSettings.instance.syncRate);
    }

    public void sync() {
        if(syncPosition && gameObject.transform.position != lastPosition) {
            emitPositionSync();
        }
        if (syncRotation && gameObject.transform.rotation.eulerAngles != lastRotation) {
            emitRotationSync();
        }

        lastPosition = gameObject.transform.position;
        lastRotation = gameObject.transform.rotation.eulerAngles;
    }

    public void emitPositionSync() {
        SyncMessage message = new SyncMessage();
        message.objectID = obj.objectID;
        message.stype = 0;
        message.posX = gameObject.transform.position.x;
        message.posY = gameObject.transform.position.y;
        message.posZ = gameObject.transform.position.z;
        message.index = index;
        WorkerServerManager.instance.sendMessageToAll(message);
    }

    public void emitRotationSync() {
        SyncMessage message = new SyncMessage();
        message.objectID = obj.objectID;
        message.stype = 1;
        message.rotX = gameObject.transform.rotation.eulerAngles.x;
        message.rotY = gameObject.transform.rotation.eulerAngles.y;
        message.rotZ = gameObject.transform.rotation.eulerAngles.z;
        message.index = index;
        WorkerServerManager.instance.sendMessageToAll(message);
    }
    #endregion

    #region Client Part
    public override void handleClientObjectSpawn() {
        if (obj.objectType != BaseQTObject.type.CLIENT) { return; }
        syncedPosition = gameObject.transform.position;
        syncedRotation = gameObject.transform.rotation.eulerAngles;
    }

    void Update() {
        if (obj.objectType != BaseQTObject.type.CLIENT) { return; }
        Vector3 newPosition = syncedPosition;
        Vector3 newRotation = syncedRotation;

        if (useInterpolation) {
            newPosition.x = calculateInterpolate(syncedPosition.x, gameObject.transform.position.x);
            newPosition.y = calculateInterpolate(syncedPosition.y, gameObject.transform.position.y);
            newPosition.z = calculateInterpolate(syncedPosition.z, gameObject.transform.position.z);
            newRotation.z = calculateInterpolate(syncedRotation.x, gameObject.transform.rotation.eulerAngles.x);
            newRotation.z = calculateInterpolate(syncedRotation.y, gameObject.transform.rotation.eulerAngles.y);
            newRotation.z = calculateInterpolate(syncedRotation.z, gameObject.transform.rotation.eulerAngles.z);
        }

        gameObject.transform.SetPositionAndRotation(newPosition, Quaternion.Euler(newRotation));
    }

    public float calculateInterpolate(float syncedValue, float realValue) {
        float value = realValue;
        float diff = Mathf.Abs(syncedValue - realValue);
        if (diff < 0.1f) {
            value = syncedValue;
        } else {
            value += diff * Time.deltaTime * 1f;
            value = Mathf.Clamp(value, -syncedValue, syncedValue);
        }

        return value;
    }
    #endregion
}
