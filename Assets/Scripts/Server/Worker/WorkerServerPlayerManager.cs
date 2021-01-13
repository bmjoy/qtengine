using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerServerPlayerManager {

    public Dictionary<string, ServerQTObject> players;

    public WorkerServerPlayerManager() {
        players = new Dictionary<string, ServerQTObject>();
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
}
