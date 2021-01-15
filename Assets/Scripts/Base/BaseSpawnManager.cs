using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawnManager {

    public Dictionary<string, BaseQTObject> spawnedObjects;
    public Dictionary<string, SyncGameobject> syncedObjects;

    public BaseSpawnManager() {
        spawnedObjects = new Dictionary<string, BaseQTObject>();
        syncedObjects = new Dictionary<string, SyncGameobject>();
    }

    public abstract BaseQTObject spawnObject(string objectID, string prefabName, SyncMessage spawnPosition);

    public void processSpawn(BaseQTObject obj) {
        spawnedObjects.Add(obj.objectID, obj);

        BaseQTObjectComponent[] objectComponents = obj.GetComponentsInChildren<BaseQTObjectComponent>(true);
        foreach(BaseQTObjectComponent comp in objectComponents) {
            comp.obj = obj;
            comp.handleSpawn();
        }

        SyncGameobject[] syncedComponents = obj.GetComponentsInChildren<SyncGameobject>(true);
        for(int i = 0; i < syncedComponents.Length; i++) {
            SyncGameobject syncComponent = syncedComponents[i];

            obj.objectComponents[i] = syncComponent;
            syncComponent.index = i;
        }
    }

    public abstract void despawnObject(string objectID);

    public void despawnAllObjects() {
        foreach(BaseQTObject obj in spawnedObjects.Values) {
            despawnObject(obj.objectID);
        }
    }

    public void processDespawn(string objectID) {
        BaseQTObject obj = spawnedObjects[objectID];
        foreach(BaseQTObjectComponent comp in obj.objectComponents.Values) {
            comp.CancelInvoke();
        }
        obj.CancelInvoke();

        QTUtils.despawnGameobject(obj.gameObject);
        spawnedObjects.Remove(objectID);
    }
}
