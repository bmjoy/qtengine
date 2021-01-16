using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerServerSpawnManager : BaseSpawnManager {

    public WorkerServerSpawnManager() : base() {

    }

    public override BaseQTObject spawnObject(string objectID, string prefabName, SyncPositionMessage spawnPosition, SyncRotationMessage spawnRotation) {
        GameObject obj = QTUtils.spawnGameobject(AppManager.instance.networkStorage.prefabs.Find(o => o.name == prefabName), spawnPosition, spawnRotation);

        ServerQTObject spawnedObj = obj.AddComponent<ServerQTObject>();
        spawnedObj.objectID = objectID;
        spawnedObj.prefabName = prefabName;
        processSpawn(spawnedObj);

        SpawnMessage message = new SpawnMessage();
        message.objectID = spawnedObj.objectID;
        message.prefabName = prefabName;
        message.spawnPosition = spawnPosition;
        message.spawnRotation = spawnRotation;
        WorkerServerManager.instance.sendMessageToAllReady(message);

        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Spawned " + prefabName + " with ID - " + objectID);

        return spawnedObj;
    }

    public override void despawnObject(string objectID) {
        ServerQTObject despawnedObj = (ServerQTObject)spawnedObjects[objectID];

        DespawnMessage message = new DespawnMessage();
        message.objectID = despawnedObj.objectID;
        WorkerServerManager.instance.sendMessageToAllReady(message);

        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Despawned " + despawnedObj.prefabName + " with ID - " + objectID);
        processDespawn(objectID);
    }
}
