using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedRoomInfo : BaseQTObjectComponent {

    public static SyncedRoomInfo instance { get; protected set; }

    [QTSynced]
    public Dictionary<string, string> players;

    //Server variables
    [QTSynced]
    public int playerNumber = 0;

    public override void handleObjectSpawn() {
        instance = this;
        players = new Dictionary<string, string>();
    }

    public override void handleServerUpdate() {
        foreach(ServerQTObject obj in WorkerServerPlayerManager.instance.players.Values) {
            if(players.ContainsKey(obj.objectID) == false) {
                players.Add(obj.objectID, "Player " + playerNumber);
                serverComponent.onSyncedFieldChanged("players", players);

                playerNumber++;
            }
        }
    }
}
