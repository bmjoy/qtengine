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
        onApplicationExit += handleApplicationQuit;
        onApplicationExit += handleMasterApplicationQuit;

        instance = this;
    }

    public void handleMasterStart() {
        database = new BaseQTDatabase();
        connections = new MasterServerConnectionsManager(this);

        webServer = new WebServer(this);
        workersManager = new MasterServerWorkersManager();
    }

    public void handleMasterApplicationQuit() {
        foreach (MasterServerWorker worker in workersManager.workers.Values) {
            worker.stop();
        }

        if (webServer.server != null) {
            webServer.server.Stop();
        }

        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Closing master server...");
    }

    public override void setupServer(int port) {
        setupTCPServer(port);
        webServer.setupWebServer();
        workersManager.spawnWorker("__global");
        database.connect();

        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Started master server on port " + port + "...");
        state = componentState.RUNNING;
    }
}
