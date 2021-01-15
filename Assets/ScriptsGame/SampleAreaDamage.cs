using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAreaDamage : BaseQTObjectComponent {

    public override void handleServerTriggerStay(Collider other) {
        if (other.gameObject.layer == 6) {
            SamplePlayer player = other.gameObject.GetComponent<SamplePlayer>();
            player.health = Mathf.Clamp(player.health - Time.deltaTime * 8f, 0, player.maxHealth);
        }
    }

}
