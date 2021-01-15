using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleHealthPack : BaseQTObjectComponent {

    public override void handleServerTriggerEnter(Collider other) {
        if(other.gameObject.layer == 6) {
            SamplePlayer player = other.gameObject.GetComponent<SamplePlayer>();
            player.health = Mathf.Clamp(player.health + 20, 0, player.maxHealth);
            WorkerServerManager.instance.spawnManager.despawnObject(obj.objectID);
        }
    }

}
