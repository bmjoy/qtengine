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
        SyncMessage message = QTUtils.getSyncMessage(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        ServerQTObject obj = (ServerQTObject)WorkerServerManager.instance.spawnManager.spawnObject(Guid.NewGuid().ToString(), prefabName, message);
    }
}
