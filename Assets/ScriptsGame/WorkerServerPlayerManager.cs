using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerServerPlayerManager : MonoBehaviour {

    public Dictionary<string, ServerQTObject> players;

    void Awake() {
        WorkerServerManager.instance.onServerSceneLoad += spawnRoomObjects;
    }

    void Start() {
        players = new Dictionary<string, ServerQTObject>();

        WorkerServerManager.instance.onClientReady += spawnPlayer;
        WorkerServerManager.instance.onClientDisconnected += despawnPlayer;
    }

    public void spawnPlayer(ServerQTClient client) {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Spawning player for Client(" + client.getIP() + ")...");

        ServerQTObject obj = (ServerQTObject)WorkerServerManager.instance.spawnManager.spawnObject(Guid.NewGuid().ToString(), "Player", QTUtils.getSyncMessage(0f, 3f, 0f));
        obj.setServerOwner(client.session.id);

        players.Add(client.session.id, obj);
    }

    public void despawnPlayer(ServerQTClient client) {
        if (players.ContainsKey(client.session.id) == false) { return; }
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Despawning player of Client(" + client.getIP() + ")...");

        ServerQTObject obj = players[client.session.id];
        WorkerServerManager.instance.spawnManager.despawnObject(obj.objectID);

        players.Remove(client.session.id);
    }

    public void spawnRoomObjects() {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Spawning objects for room...");

        ServerQTObject obj = (ServerQTObject)WorkerServerManager.instance.spawnManager.spawnObject(Guid.NewGuid().ToString(), "HealthSpawner", QTUtils.getSyncMessage(30f, 1f, 4f));
        ServerQTObject obj2 = (ServerQTObject)WorkerServerManager.instance.spawnManager.spawnObject(Guid.NewGuid().ToString(), "AreaDamage", QTUtils.getSyncMessage(-5f, 2f, -13f));
    }
}
