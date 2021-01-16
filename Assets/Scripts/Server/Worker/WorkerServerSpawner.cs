using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerServerSpawner : MonoBehaviour {

    public string prefabName;

    void Awake() {
        WorkerServerManager.instance.onServerSceneLoad += spawnObject;
    }

    public void spawnObject() {
        SyncPositionMessage positionMessage = QTUtils.getSyncPositionMessage(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        SyncRotationMessage rotationMessage = QTUtils.getSyncRotationMessage(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z);

        ServerQTObject obj = (ServerQTObject)WorkerServerManager.instance.spawnManager.spawnObject(Guid.NewGuid().ToString(), prefabName, positionMessage, rotationMessage);
    }
}
