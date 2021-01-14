using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedComponentTest : BaseQTObjectComponent {

    [QTSynced]
    public float syncedFloat;

    void Start() {
        obj = gameObject.AddComponent<ServerQTObject>();
        handleSpawn();
    }

    public override void handleObjectSpawn() {

    }

    void Update() {
        syncedFloat = Time.time;
    }
}
