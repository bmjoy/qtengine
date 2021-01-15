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
        for (int i = 0; i < objectComponents.Length; i++) {
            BaseQTObjectComponent comp = objectComponents[i];

            comp.obj = obj;
            comp.index = i;
            comp.handleSpawn();

            obj.objectComponents.Add(i, comp);
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
