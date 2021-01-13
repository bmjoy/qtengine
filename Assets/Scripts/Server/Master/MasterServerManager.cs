using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterServerManager : BaseServerManager {

    public static MasterServerManager instance { get; protected set; }
    public MasterServerWorkersManager workersManager;
    public WebServer webServer;

    void Awake() {
        onStart += handleStart;
        onStart += handleMasterStart;
        onUpdate += handleUpdate;
        onApplicationQuit += handleApplicationQuit;
        onApplicationQuit += handleMasterApplicationQuit;

        instance = this;
    }

    public void handleMasterStart() {
        connections = new MasterServerConnectionsManager(this);

        webServer = new WebServer(this);
        workersManager = new MasterServerWorkersManager();

        onClientDisconnected += connections.handleClientDisconnect;
        onClientConnected += connections.handleClientConnect;
    }

    public void handleMasterApplicationQuit() {
        foreach(MasterServerWorker worker in workersManager.workers.Values) {
            worker.stop();
        }

        webServer.server.Stop();
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Closing master server...");
    }

    public override void setupServer(int port) {
        setupTCPServer(port);
        webServer.setupWebServer();
        workersManager.spawnWorker("__global");

        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Started master server on port " + port + "...");
        state = componentState.RUNNING;
    }
}
