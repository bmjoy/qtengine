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
    public List<BaseQTClient> clients;
    public Thread thread;

    public double lastHeartbeatRequest;

    public Action processQueue;

    public BaseServerConnectionsManager(BaseServerManager _manager) {
        manager = _manager;
        clients = new List<BaseQTClient>();

        manager.onClientConnected += handleClientConnect;
        manager.onClientDisconnected += handleClientDisconnect;
        manager.onClientConnected += debugNewConnection;
        manager.onClientDisconnected += debugLostConnection;

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

    public void handleClientConnect(BaseQTClient client) {
        SessionInfo session = new SessionInfo();
        session.id = Guid.NewGuid().ToString();
        client.session = session;

        clients.Add(client);
    }

    public void handleClientDisconnect(BaseQTClient client) {
        clients.Remove(client);
    }

    public void debugNewConnection(BaseQTClient client) {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Client(" + client.getIP() + ") connected...");
    }

    public void debugLostConnection(BaseQTClient client) {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Client(" + client.getIP() + ") disconnected...");
    }
}
