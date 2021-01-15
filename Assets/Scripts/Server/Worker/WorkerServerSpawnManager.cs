using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerServerSpawnManager : BaseSpawnManager {

    public WorkerServerSpawnManager() : base() {

    }

    public override BaseQTObject spawnObject(string objectID, string prefabName, SyncMessage spawnPosition) {
        GameObject obj = QTUtils.spawnGameobject(AppManager.instance.networkStorage.prefabs.Find(o => o.name == prefabName), spawnPosition);

        ServerQTObject spawnedObj = obj.AddComponent<ServerQTObject>();
        spawnedObj.objectID = objectID;
        spawnedObj.prefabName = prefabName;
        processSpawn(spawnedObj);

        SpawnMessage message = new SpawnMessage();
        message.objectID = spawnedObj.objectID;
        message.prefabName = prefabName;
        message.spawnPosition = spawnPosition;
        WorkerServerManager.instance.sendMessageToAllReady(message);

        return spawnedObj;
    }

    public override void despawnObject(string objectID) {
        ServerQTObject despawnedObj = (ServerQTObject)spawnedObjects[objectID];

        DespawnMessage message = new DespawnMessage();
        message.objectID = despawnedObj.objectID;
        WorkerServerManager.instance.sendMessageToAllReady(message);

        QTUtils.despawnGameobject(despawnedObj.gameObject);
        processDespawn(objectID);
    }
}
