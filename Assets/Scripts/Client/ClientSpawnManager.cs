using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawnManager : BaseSpawnManager {

    public ClientSpawnManager() : base() {

    }

    public override BaseQTObject spawnObject(string objectID, string prefabName, SyncMessage spawnPosition) {
        GameObject obj = QTUtils.spawnGameobject(AppManager.instance.networkStorage.prefabs.Find(o => o.name == prefabName), spawnPosition);
        ClientQTObject spawnedObj = obj.AddComponent<ClientQTObject>();
        spawnedObj.objectID = objectID;
        spawnedObj.prefabName = prefabName;
        processSpawn(spawnedObj);

        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Spawned " + prefabName + " with ID - " + objectID);

        return spawnedObj;
    }

    public override void despawnObject(string objectID) {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Despawned " + spawnedObjects[objectID].prefabName + " with ID - " + objectID);
        processDespawn(objectID);
    }
}
