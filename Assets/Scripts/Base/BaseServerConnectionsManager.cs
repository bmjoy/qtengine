using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public abstract class BaseServerConnectionsManager {

    public BaseServerManager manager;
    public List<ServerQTClient> clients;
    public Thread thread;

    public double lastHeartbeatRequest;

    public Action processQueue;

    public BaseServerConnectionsManager(BaseServerManager _manager) {
        manager = _manager;

        clients = new List<ServerQTClient>();

        manager.onClientConnected += handleClientConnect;
        manager.onClientDisconnected += handleClientDisconnect;
        manager.onClientConnected += debugNewConnection;
        manager.onClientDisconnected += debugLostConnection;

        manager.onMessageReceived += handleHeartbeat;

        processQueue += handleQueues;
    }

    public void handleQueues() {
        foreach (ServerQTClient client in clients) {
            client.processQueue();
        }
    }

    public void startHandlingConnects() {
        thread = new Thread(new ThreadStart(handleConnects));
        thread.Start();
    }

    public abstract void handleConnects();

    public abstract void handleDisconnects();

    public void handleClientConnect(ServerQTClient client) {
        SessionInfo session = new SessionInfo();
        session.id = Guid.NewGuid().ToString();
        client.session = session;
    }

    public void handleClientDisconnect(ServerQTClient client) {
        clients.Remove(client);
    }

    public void handleHeartbeat(ServerQTClient client, QTMessage message) {
        double currentTimestamp = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
        client.lastHeartbeatTimestamp = currentTimestamp;
    }

    public void debugNewConnection(ServerQTClient client) {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Client(" + client.getIP() + ") connected...");
    }

    public void debugLostConnection(ServerQTClient client) {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Client(" + client.getIP() + ") disconnected...");
    }
}
