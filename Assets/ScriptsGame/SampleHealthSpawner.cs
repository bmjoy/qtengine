using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleHealthSpawner : BaseQTObjectComponent {

    //Server variables
    public ServerQTObject spawnedObject = null;
    public float elapsedTime = 0f;

    public override void handleServerUpdate() {
        if(spawnedObject != null) {
            spawnedObject.gameObject.transform.Rotate(new Vector3(0f, 30f * Time.deltaTime, 0f));
            elapsedTime = 0f;
        } else {
            elapsedTime += Time.deltaTime;
        }

        if(elapsedTime >= 10f) {
            elapsedTime = 0f;
            spawnPack();
        }
    }
    public void spawnPack() {
        ServerQTObject spawnedObj = (ServerQTObject)WorkerServerManager.instance.spawnManager.spawnObject(Guid.NewGuid().ToString(), "HealthPack", QTUtils.getSyncMessageFromObject(obj));
        spawnedObject = spawnedObj;
    }

}
